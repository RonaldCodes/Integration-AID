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

namespace Agent.Integration
{
    [Obsolete]
    public class RttStyleModelFactory : IUploadModelFactory
    {
        private readonly Utils utils;

        public RttStyleModelFactory()
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
            var route = new UploadModel();

            if (site == null) return null;

            foreach (var consignment in trip)
            {
                var action = CreateAction(consignment, site, details);

                consignment.Parcels.ForEach(p =>
                route.Add(new HandlingUnit
                {
                    Id = $"{site.Id}/unit/{p.ParcelID?.Trim()}",
                    Barcode = p.Barcode?.Trim(),
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

                var deco = CreateDeco(consignment, site);

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

            route.Add(new EntityContactsDescriptor { Authority = EAuthority.Trackmatic });

            route.Route = CreateRoute(details, site);

            route.Route.Personnel.Add(new Personnel
            {
                Id = $"{site.Id}/personnel/{details.Driver.IDNo}",
                Name = details.Driver.Name?.Trim(),
                IdentityNumber = details.Driver.IDNo.ToString()
            });

            return route;
        }

        private Action CreateAction(Consignment consignment, RttSiteDefault site, RTTTripDetails details)
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
                Extensions = new List<ExtensionProperty>()
                {
                    //CreateProperty(consignment)
                }
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

        private ExtensionProperty CreateProperty(Consignment consignment)
        {
            var check = consignment.References.Select(x => x.ref_type == 1);
            if (check.Any())
            {
                var value = consignment.References.Where(x => x.ref_type == 1).FirstOrDefault();
                if (value != null)
                {
                    return new ExtensionProperty()
                    {
                        Key = "Reference",
                        Value = value.refno
                    };
                }
            }
            return null;
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

        private Entity CreateTempSellTo(Consignment consignment, RttSiteDefault site)
        {
            var entity = new Entity
            {
                Id = utils.CreateSellToId(consignment, site),
                Reference = consignment.Account.AccountNo?.Trim(),
                Name = consignment.Account.account_name?.Trim(),
            };

            entity.Requirements.Add(new RequireActionDebrief());
            entity.Requirements.Add(new RequireHandlingUnitDebrief());

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
            entity.Requirements.Add(new RequireHandlingUnitDebrief());

            return entity;
        }

        private OLocation CreateDeco(Consignment consignment, RttSiteDefault site)
        {
            var deco = new OLocation
            {
                Name = consignment.Address.Name?.Trim(),
                Id = utils.CreateDecoId(consignment, site),// $"{site.Id}/{consignment.Address.Addr_Id}",
                ClientId = site.Id,
                Reference = consignment.Address.Addr_id.GetValueOrDefault().ToString(),
                DefaultStopTime = TimeSpan.FromMinutes(20),
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                {
                    new OCoord
                    {
                        Latitude = Convert.ToDouble(string.IsNullOrEmpty(consignment.Address.GpsLat) ? "0" : consignment.Address.GpsLat),
                        Longitude = Convert.ToDouble(string.IsNullOrEmpty(consignment.Address.GpsLong) ? "0" : consignment.Address.GpsLong),
                        Radius = 200
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
                    Radius = 200
                }
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
                StartDate = details.TripDate,
                TemplateId = site.RttTemplateId
            };
            return route;
        }
    }
}
