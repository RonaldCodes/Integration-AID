using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Model;
using Trackmatic.Rest;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Model;
using Action = Trackmatic.Rest.Routing.Model.Action;
using Personnel = Trackmatic.Rest.Routing.Model.Personnel;
using Trackmatic.Rest.Routing.Model.Descriptors;
using System.Collections.ObjectModel;

namespace Agent.Integration
{
    public class RttTarsusModelFactory : IUploadModelFactory
    {
        private readonly Utils utils;

        public RttTarsusModelFactory()
        {
            utils = new Utils();
        }

        public UploadModel Create(RTTTripDetails details, RttSiteDefault site)
        {
            var trips = details.Consignments.ToList();
            var orderedList = trips.OrderBy(x => x.StopNumber).ToList();
            var results = Translate(orderedList, site, details);
            return results;
        }

        private UploadModel Translate(List<Consignment> trip, RttSiteDefault site, RTTTripDetails details)
        {
            var planModel = new PlanModel();
            var route = new UploadModel();
            var decosToCheck = trip.Where(x => x.ConsType.ToLower() != "depot").ToList();
            decosToCheck.Add(trip[0]);
            var timeIsValid = decosToCheck.All(x => CheckForInvalidTimes(details, x));
            var lastStopIsValid = IsLastStopInvalid(trip[trip.Count-1]);
            var depots = trip.Where(x => x.ConsType.ToLower() == "depot").ToList();

            if (timeIsValid && lastStopIsValid)
            {
                route.Add(site.StaticDeco);
                planModel.Stops.Add(new StopModel
                {
                    LocationId = site.StaticDeco.Id,
                    DueTime = depots[0].PlannedStopDepartureTime.GetValueOrDefault()
                });
            }

            if (site == null) return null;
            foreach (var consignment in trip)
            {
                if (consignment.ConsType.ToLower() == "depot")
                {
                    continue;
                }

                var action = CreateAction(details, consignment, site);

                consignment.Parcels.ForEach(p =>
                route.Add(new HandlingUnit
                {
                    Id = $"{site.Id}/unit/{p.ParcelID?.Trim()}",
                    Barcode = p.Barcode,
                    CustomerReference = p.ConsID.ToString(),
                    Quantity = 1,
                    Weight = p.ActualKG,
                    Dimensions = new HandlingUnitDimensions
                    {
                        Height = p.Height,
                        Length = p.Length,
                        Width = p.Width
                    },
                    Status = EHandlingUnitStatus.Pending
                }));

                route.Add(action);

                var sellTo = CreateSellTo(consignment, site);

                var deco = CreateDeco(consignment, site, details);

                route.Add(deco);

                var shipTo = CreateShipTo(consignment.Address, site);
                route.Add(shipTo);
                route.Add(sellTo);

                var relationship = Relationship
                    .Link(action)
                    .To(shipTo)
                    .At(deco)
                    .SellTo(sellTo);
                route.Add(relationship);
            }

            foreach (var consignments in trip.Where(x => x.ConsType.ToLower() != "depot").GroupBy(x => x.Address.Addr_id))
            {
                var stopModel = CreateStopModel(consignments.ToList(), site);
                planModel.Stops.Add(stopModel);
            }

            route.Add(new EntityContactsDescriptor { Authority = EAuthority.Trackmatic });
            route.Add(new RelationshipMstDescriptor { Authority = EAuthority.Lob });
            route.Add(new LocationDefaultStopTimeDescriptor { Authority = EAuthority.Lob });
            route.Add(new LocationExtensionsDescriptor { Authority = EAuthority.Lob });

            route.Route = CreateRoute(details, site);

            route.Route.Personnel.Add(new Personnel
            {
                Id = $"{site.Id}/personnel/{details.Driver.IDNo?.ToString()}",
                Name = details.Driver.Name?.Trim(),
                IdentityNumber = details.Driver.IDNo?.ToString()
            });

            if (timeIsValid && lastStopIsValid)
            {
                planModel.Stops.Add(new StopModel
                {
                    LocationId = site.StaticDeco.Id,
                    DueTime = depots[1].PlannedStopArrivalTime.GetValueOrDefault()
                });

                route.Plan = planModel;
                route.Route.StartDate = DeterminePlanModelDate(details);
                route.Route.IsLocked = true;
            }
            else
            {
                route = CreateUniqueStopTime(route, 8);
            }
            return route;
        }

        public bool IsPlannedRoute(RTTTripDetails trip)
        { 
            if (trip.PlannedRoute == true) return true;
            return false;
        }

        private UploadModel CreateUniqueStopTime(UploadModel model, int minutes)
        {
            foreach (var stop in model.Relationships.GroupBy(x => x.DecoId))
            {
                var entities = stop.GroupBy(x => x.EntityId).Count();

                foreach (var relationship in stop)
                {
                    relationship.Mst = TimeSpan.FromMinutes(minutes / entities);
                    relationship.MstOverride = TimeSpan.FromMinutes(minutes);
                }
            }
            return model;
        }

        private bool CheckForInvalidTimes(RTTTripDetails details,  Consignment consignment)
        {
            var minValue = DateTime.MinValue;
            if (consignment.PlannedStopArrivalTime == minValue || consignment.PlannedStopDepartureTime == minValue || details.PlannedTripStartAt == minValue
                || consignment.PlannedStopArrivalTime == null || consignment.PlannedStopDepartureTime == null || details.PlannedTripStartAt == null)
            {
                return false;
            }
            return true;
        }

        private bool IsLastStopInvalid(Consignment consignment)
        {
            if (consignment.PlannedStopArrivalTime == null)
            {
                return false;
            }
            return true;
        }

        private StopModel CreateStopModel(List<Consignment> consignments, RttSiteDefault site)
        {
            var stopModel = new StopModel
            {
                DueTime = consignments[0].PlannedStopArrivalTime.GetValueOrDefault(),
                LocationId = utils.ResolveDecoId(consignments[0], site),
                TimeAtStop = SumConsignmentStopTimes(consignments),
                Visits = new List<StopVisitModel>()
                {
                    CreateStopVisitModel(consignments, site)
                },
                PathToStop = new PathModel()
                {
                    Distance = consignments[0].PlannedTravelDistance,
                    TravelTime = TimeSpan.FromMinutes(consignments[0].PlannedTravelDuration)
                }
            };

            return stopModel;
        }

        private TimeSpan SumConsignmentStopTimes(List<Consignment> consignments)
        {
            var total = TimeSpan.Zero;
            foreach (var consignment in consignments)
            {
                return DetermineStopTime(consignment.PlannedStopOffloadDuration);
            }
            return total;
        }

        private StopVisitModel CreateStopVisitModel(List<Consignment> consignments, RttSiteDefault site)
        {
            return new StopVisitModel()
            {
                EntityId = utils.CreateShipToId(consignments[0].Address, site),
                Actions = consignments.Select(x => utils.CreateActionId(x, site)).ToList(),
                MaxServiceTime = SumConsignmentStopTimes(consignments)
            };
        }

        private TimeSpan DetermineStopTime(int PlannedOffloadDuration)
        {
            if (PlannedOffloadDuration == 0)
            {
                return TimeSpan.FromMinutes(8);
            }
            return TimeSpan.FromMinutes(PlannedOffloadDuration);
        }

        private Action CreateAction(RTTTripDetails details, Consignment consignment, RttSiteDefault site)
        {
            var action = new Action
            {
                Id = utils.CreateActionId(consignment, site),
                InternalReference = consignment.ConsignmentID.ToString(),
                Reference = consignment.ConsignmentNo?.Trim(),
                CreatedOn = details.TripDate.GetValueOrDefault(),
                ExpectedDelivery = details.TripDate,
                ClientId = site.Id,
                CustomerReference = consignment.Account.Acc_Id.ToString(),
                CustomerCode = consignment.Account.AccountNo?.Trim(),
                IsCod = false,
                AmountIncl = 0,
                Nature = EActionNature.Product,
                Weight = consignment.Parcels.Sum(p => p.ActualKG),
                Pieces = consignment.Parcels.Count,
                Volume = consignment.Parcels.Sum(p => p.Length * p.Height * p.Width),
                VolumetricMass = consignment.Parcels.Sum(p => p.VolumizerKG),
                HandlingUnitIds = consignment.Parcels.Select(p => $"{site.Id}/unit/{p.ParcelID?.Trim()}").ToList(),
            };

            if (consignment.Parcels.Count == 0)
            {
                action.ActionTypeId = site.RttActionTypeCollection;
                action.ActionTypeName = "Collection";
            }
            else
            {
                action.ActionTypeId = site.RttActionTypeDelivery;
                action.ActionTypeName = "Delivery";
            }

            return action;
        }

        private Entity CreateSellTo(Consignment consignment, RttSiteDefault site)
        {
            var entity = new Entity
            {
                Id = utils.CreateSellToId(consignment, site),
                Reference = consignment.Account.AccountNo?.Trim(),
                Name = consignment.Account.account_name?.Trim()
            };

            return entity;
        }

        private Entity CreateShipTo(Model.Address address, RttSiteDefault site)
        {
            var entity = new Entity
            {
                Id = utils.CreateShipToId(address, site),
                Reference = address.Addr_id.ToString(),
                Name = address.Name
            };

            entity.Requirements.Add(new RequireActionDebrief());
            entity.Requirements.Add(new RequireSignature());

            return entity;
        }

        private OLocation CreateDeco(Consignment consignment, RttSiteDefault site, RTTTripDetails trip)
        {
            var deco = new OLocation
            {
                Name = consignment.Address.Name?.Trim(),
                Id = utils.ResolveDecoId(consignment, site),
                ClientId = site.Id,
                Reference = consignment.Address.Addr_id.ToString(),
                DefaultStopTime = DetermineStopTime(consignment.PlannedStopOffloadDuration),
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                {
                    new OCoord
                    {
                        Latitude = Convert.ToDouble(string.IsNullOrEmpty(consignment.Address.GpsLat) ? "0" : consignment.Address.GpsLat),
                        Longitude = Convert.ToDouble(string.IsNullOrEmpty(consignment.Address.GpsLong) ? "0" : consignment.Address.GpsLong),
                        Radius = 100
                    }
                },
                StructuredAddress = new StructuredAddress
                {
                    Street = consignment.Address.Address1?.Trim(),
                    Suburb = consignment.Address.Suburb?.Trim(),
                    City = consignment.Address.Town?.Trim(),
                    PostalCode = consignment.Address.Postal?.Trim()
                },
                Entrance = new OCoord
                {
                    Latitude = Convert.ToDouble(string.IsNullOrEmpty(consignment.Address.GpsLat) ? "0" : consignment.Address.GpsLat),
                    Longitude = Convert.ToDouble(string.IsNullOrEmpty(consignment.Address.GpsLong) ? "0" : consignment.Address.GpsLong),
                    Radius = 100
                },
                Extensions = CreateStopExtensions(trip, consignment)
            };

            return deco;
        }

        private RouteModel CreateRoute(RTTTripDetails details, RttSiteDefault site)
        {
            var route = new RouteModel
            {
                Name = details.TripHeaderID,
                Id = $"{site.Id}/{details.TripHeaderID}",
                Registration = details.Vehicle.VehRegno.CleanVehicleReg(),
                Reference = details.TripHeaderID,
                Schedule = true,
                StartDate = CreateDefaultStartDate(details),
                TemplateId = site.RttTemplateId,
                Extensions = CreateRouteExtensions(details)
            };
            return route;
        }

        private ObservableCollection<ExtensionProperty> CreateRouteExtensions(RTTTripDetails trip)
        {
            if (trip.PlannedRoute == true)
            {
                return new ObservableCollection<ExtensionProperty>()
                {
                    new ExtensionProperty()
                    {
                        Key = "SizwePlannedStart",
                        Value = trip.GetSizwePlannedStartDate()
                    },
                    new ExtensionProperty()
                    {
                        Key = "SizwePlannedEnd",
                        Value = trip.GetSizwePlannedEndDate()
                    },
                    new ExtensionProperty()
                    {
                        Key = "SizwePlannedRouteId",
                        Value = trip.GetSizwePlannedTripId()
                    },
                    new ExtensionProperty()
                    {
                        Key = "PlannedVehicleClass",
                        Value = trip.GetPlannedVehicleClass()
                    }
                };
            }
            return null;
        }

        private SpecializedObservableCollection<ExtensionProperty> CreateStopExtensions(RTTTripDetails trip, Consignment consignment)
        {
            if (trip.PlannedRoute == true)
            {
                return new SpecializedObservableCollection<ExtensionProperty>()
                {
                    new ExtensionProperty()
                    {
                        Key = "SizweOffloadStart",
                        Value = consignment.GetSizwePlannedOffloadStart()
                    },
                    new ExtensionProperty()
                    {
                        Key = "SizweOffloadEnd",
                        Value = consignment.GetSizwePlannedOffloadEnd()
                    },
                    new ExtensionProperty()
                    {
                        Key = "SizweOffloadDuration",
                        Value = consignment.GetSizwePlannedOffloadDuration()
                    },
                    new ExtensionProperty()
                    {
                        Key = "SizweDistance",
                        Value = consignment.GetSizwePlannedDistance()
                    },
                    new ExtensionProperty()
                    {
                        Key = "SizweAddressId",
                        Value = consignment.GetSizweAddressId()
                    },
                };
            }
            return null;
        }

        public DateTime CreateDefaultStartDate(RTTTripDetails details)
        {
            var date = details.PlannedTripStartAt.GetValueOrDefault();
            var tripDate = details.TripDate.GetValueOrDefault();

            if (details.PlannedRoute == false)
            {
                if (tripDate.Hour >= 12 && tripDate.Hour <= 16)
                {
                    return new DateTime(tripDate.Year, tripDate.Month, tripDate.Day, 14, 0, 0).ToUniversalTime();
                }
                if (tripDate.Hour > 18)
                {
                    return new DateTime(tripDate.Year, tripDate.Month, tripDate.Day, 8, 0, 0).ToUniversalTime().AddDays(1);
                }
                return new DateTime(tripDate.Year, tripDate.Month, tripDate.Day, 8, 0, 0).ToUniversalTime();
            }
            if (date.Hour >= 12)
            {
                return new DateTime(date.Year, date.Month, date.Day, 14, 0, 0).ToUniversalTime();
            }
            return new DateTime(date.Year, date.Month, date.Day, 8, 0, 0).ToUniversalTime();
        }

        public DateTime DeterminePlanModelDate(RTTTripDetails details)
        {
            var minValue = DateTime.MinValue;
            if (details.PlannedTripStartAt == minValue || details.PlannedTripStartAt == null) return details.TripDate.GetValueOrDefault().ToUniversalTime();
            if (details.PlannedTripStartAt.GetValueOrDefault().Year < 2000)
            {
                var tripDate = details.TripDate.GetValueOrDefault();
                return new DateTime(tripDate.Year, tripDate.Month, tripDate.Day, 8, 0, 0).ToUniversalTime();
            }
            return details.PlannedTripStartAt.GetValueOrDefault().ToUniversalTime();
        }
    }
}
