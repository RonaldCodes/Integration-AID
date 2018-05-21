using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Model;
using Trackmatic.Rest;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Model;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace Agent.Integration
{
    public class RttStyleConsolidationModelFactory : IUploadModelFactory
    {
        private readonly Utils utils;

        public RttStyleConsolidationModelFactory()
        {
            utils = new Utils();
        }

        public UploadModel Create(RTTTripDetails details, RttSiteDefault site)
        {
            var data = details.Consignments.OrderBy(x => x.StopNumber).ToList();
            var orderedList = data.GroupBy(p => GetGroupId(p)).ToList();
            var results = TranslateGrouping(details, orderedList, site);
            return results;
        }

        private UploadModel TranslateGrouping(RTTTripDetails details, List<IGrouping<string, Consignment>> grouping, RttSiteDefault site)
        {
            var model = new UploadModel();

            if (site == null) return null;

            foreach (var group in grouping)
            {
                var route = CreateGroupRoute(details, site);
                var action = CreateGroupAction(details, group, site);
                var deco = CreateGroupDeco(group, site);
                var shipTo = CreateGroupShipTo(group, site);
                var sellTo = CreateGroupSellTo(group, site);

                model.Add(action);
                model.Add(deco);
                model.Add(shipTo);
                model.Add(sellTo);
                model.Route = route;

                var relationship = Relationship
                    .Link(action)
                    .To(shipTo)
                    .At(deco)
                    .SellTo(sellTo);
                model.Add(relationship);

                relationship.Mst = TimeSpan.FromMinutes(15);
                relationship.MstOverride = TimeSpan.FromMinutes(15);

                model = AddHandlingUnits(group, site, model);
            }

            return model;
        }

        public UploadModel AddHandlingUnits(IGrouping<string, Consignment> group, RttSiteDefault site, UploadModel model)
        {
            foreach (var consignment in group)
            {
                consignment.Parcels.ForEach(p =>
                model.Add(new HandlingUnit
                {
                    Id = $"{site.Id}/unit/{p.ParcelID?.Trim()}",
                    Barcode = p.Barcode?.Trim(),
                    CustomerReference = p.ConsID.ToString(),
                    InternalReference = consignment.ConsignmentNo,
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
            }

            return model;
        }

        private RouteModel CreateGroupRoute(RTTTripDetails details, RttSiteDefault site)
        {
            var tripDate = details.TripDate.GetValueOrDefault();
            var date = new DateTime(tripDate.Year, tripDate.Month, tripDate.Day, 8, 0, 0).ToUniversalTime();
            var route = new RouteModel
            {
                Name = details.TripHeaderID,
                Id = $"{site.Id}/{details.TripHeaderID}",
                Registration = details.Vehicle.VehRegno.CleanVehicleReg(),
                Reference = details.TripHeaderID,
                Schedule = true,
                StartDate = date,
                TemplateId = site.RttTemplateId
            };
            return route;
        }

        private Action CreateGroupAction(RTTTripDetails details, IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            var action = new Action
            {
                Id = CreateActionId(group, site),
                InternalReference = group.Select(x => x.ConsignmentID).FirstOrDefault().ToString(),
                Reference = group.Select(x => GetGroupId(x)).FirstOrDefault().ToString(),
                CreatedOn = details.TripDate.GetValueOrDefault(),
                ExpectedDelivery = details.TripDate,
                ClientId = site.Id,
                CustomerReference = group.Select(x => x.ConsignmentID).FirstOrDefault().ToString(),
                CustomerCode = group.Select(x => x.Address.Addr_id).FirstOrDefault().ToString(),
                IsCod = false,
                AmountIncl = 0,
                Nature = EActionNature.Product,
                Weight = group.Sum(x => x.Parcels.Select(y => y.ActualKG).Sum()),
                Pieces = group.Select(x => x.TotalParcelCount).Sum(),
                VolumetricMass = group.Sum(x => x.Parcels.Select(y => y.VolumizerKG).Sum()),
                HandlingUnitIds = HandleGroup(group, site),
                Direction = EActionDirection.Outbound
            };

            if (group.Select(x => x).FirstOrDefault().IsCollection())
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

        public Entity CreateGroupShipTo(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            var entity = new Entity
            {
                Id = CreateShipToId(group, site),
                Reference = group.Select(x => x.Address.Addr_id).FirstOrDefault().ToString(),
                Name = group.Select(x => x.Address.Name).FirstOrDefault()
            };
            entity.Requirements.Add(new RequireHandlingUnitDebrief() { CaptureQuantityPerSku = true });

            return entity;
        }

        public Entity CreateGroupSellTo(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            var entity = new Entity
            {
                Id = CreateSellToId(group, site),
                Reference = group.Select(x => x.Account.AccountNo).FirstOrDefault().ToString(),
                Name = group.Select(x => x.Account.account_name).FirstOrDefault()
            };

            return entity;
        }

        public string CreateActionId(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            if (IsDelivery(group))
            {
                return $"{site.Id}/{group.Select(x => GetGroupId(x)).FirstOrDefault()}";
            }
            else
            {
                return $"{site.Id}/{group.Select(x => GetGroupId(x)).FirstOrDefault()}/C";
            }
        }

        private bool IsDelivery(IGrouping<string, Consignment> group)
        {
            return group.Any(x => x.ConsType == "Delivery");
        }

        public string CreateSellToId(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            return $"{site.Id}/entity/{group.Select(x => x.Account.AccountNo).FirstOrDefault()}";
        }

        public string CreateShipToId(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            return $"{site.Id}/entity/{group.Select(x => x.Address.Addr_id).FirstOrDefault()}";
        }

        public string CreateDecoId(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            return $"{site.Id}/{group.Select(x => x.Address.Addr_id).FirstOrDefault().GetValueOrDefault().ToString()}";
        }

        public OLocation CreateGroupDeco(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            var lat = group.Select(x => x.Address.GpsLat).FirstOrDefault();
            var lon = group.Select(x => x.Address.GpsLong).FirstOrDefault();

            var deco = new OLocation
            {
                Name = group.Select(x => x.Address.Name).FirstOrDefault(),
                Id = CreateDecoId(group, site),// $"{site.Id}/{consignment.Address.Addr_Id}",
                ClientId = site.Id,
                Reference = group.Select(x => x.Address.Addr_id).FirstOrDefault().ToString(),
                DefaultStopTime = TimeSpan.FromMinutes(20),
                Shape = EZoneShape.Radius,
                Coords = new SpecializedObservableCollection<OCoord>
                {
                    new OCoord
                    {
                        Latitude = Convert.ToDouble(string.IsNullOrWhiteSpace(lat) ? "0" : lat),
                        Longitude = Convert.ToDouble(string.IsNullOrWhiteSpace(lon) ? "0" : lon),
                        Radius = 200
                    }
                },
                StructuredAddress = new StructuredAddress
                {
                    Street = group.Select(x => x.Address.Address1).FirstOrDefault(),
                    Suburb = group.Select(x => x.Address.Suburb).FirstOrDefault(),
                    City = group.Select(x => x.Address.Town).FirstOrDefault(),
                    PostalCode = group.Select(x => x.Address.Postal).FirstOrDefault()
                },
                Entrance = new OCoord
                {
                    Latitude = Convert.ToDouble(string.IsNullOrWhiteSpace(lat) ? "0" : lat),
                    Longitude = Convert.ToDouble(string.IsNullOrWhiteSpace(lon) ? "0" : lon),
                    Radius = 200
                }
            };

            return deco;
        }

        public List<string> HandleGroup(IGrouping<string, Consignment> group, RttSiteDefault site)
        {
            var finalList = new List<string>();
            foreach (var consignment in group)
            {
                var tempList = consignment.Parcels.Select(p => $"{site.Id}/unit/{p.ParcelID.Trim()}");
                foreach (var item in tempList)
                {
                    finalList.Add(item);
                }
            }
            return finalList;
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

        private string GetGroupId(Consignment consignment)
        {
            if (consignment.TripPODNo == "0")
            {
                return consignment.ConsignmentID.ToString();
            }

            return consignment.TripPODNo;
        }
    }
}
