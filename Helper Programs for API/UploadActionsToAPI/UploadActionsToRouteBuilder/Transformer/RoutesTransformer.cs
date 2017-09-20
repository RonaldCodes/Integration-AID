using Agent.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Model;

namespace UploadActionsToRouteBuilder.Transformer
{
    public class RoutesTransformer
    {
        private readonly SiteData _site;

        private readonly List<ActionItem> _actions;
        public RoutesTransformer(SiteData site, List<ActionItem> actions)
        {
            _site = site;
            _actions = actions;
        }

        public UploadModel CreateModel()
        {
            var model = new UploadModel();

            foreach (var invoice in _actions)
            {
                var action = CreateRouteAction(_site, invoice);
                model.Add(action);
                var entity = CreateRouteEntity(_site, invoice);
                model.Add(entity);
                var deco = CreateRouteDeco(_site, invoice);
                model.Add(deco);
                var relationship = Relationship.Link(action).To(entity).At(deco);
                model.Add(relationship);
            }

            return model;
        }

        public static Trackmatic.Rest.Routing.Model.Action CreateRouteAction(SiteData _site, ActionItem action)
        {
            var routeBuilderAction = new Trackmatic.Rest.Routing.Model.Action
            {
                Id = AsId(_site.Id, action.GetActionReference()),
                Reference = action.GetActionReference(),
                CustomerReference = action.GetCustomerReference(),
                InternalReference = action.GetInternalReference(),
                ActionTypeId = action.GetActionTypeId(),
                ActionTypeName = action.GetNature(),
                ExpectedDelivery = action.GetExpectedDeliveryDate(),
                ReceivedOn = DateTime.UtcNow,
                Direction = EActionDirection.Outbound,//action.DetermineDirection(),
                Nature = EActionNature.Product,
                ClientId = _site.Id,
                Instructions = action.GetSpecialInstructions(),
                CreatedOn = action.GetCreatedOn(),
            };
            return routeBuilderAction;
        }

        private Entity CreateRouteEntity(SiteData _site, ActionItem action)  //Shipto
        {
            var entity = new Entity
            {
                Id = $"{_site.Id}/entity/{action.GetCustomerReference()}",
                Reference = action.GetCustomerReference(),
                Name = action.GetCustomerName(),
                Requirements = new System.Collections.ObjectModel.ObservableCollection<EntityRequirement>
                {
                    new RequireActionDebrief(),
                    new RequireSignature()
                },

                Contacts = new System.Collections.ObjectModel.ObservableCollection<EntityContact>()
                {
                    new EntityContact()
                    {
                        CellNo = action.GetCellNo(),
                        Email = action.GetEmail(),
                    }
                }

            };
            return entity;
        }

        private OLocation CreateRouteDeco(SiteData _site, ActionItem action)
        {
            var deco = new OLocation
            {
                Id = string.Join("/",_site.Id,action.GetCustomerName(),action.GetCustomerReference()),
                Reference = action.GetCustomerReference(),
                Name = action.GetCustomerName(),
                ClientId = _site.Id,
                 ReceivedOn = DateTime.Now,
                Coords = new Trackmatic.Rest.SpecializedObservableCollection<OCoord>()
                {
                    new OCoord()
                    {
                        Latitude = action.GetLatitude(),
                        Longitude = action.GetLongitude(),
                        Radius = 100
                    }
                },
                Entrance = new OCoord()
                {
                    Latitude = action.GetLatitude(),
                    Longitude = action.GetLongitude(),
                },
                StructuredAddress = new StructuredAddress()
                {
                    UnitNo = action.GetUnitNo(),
                    BuildingName = action.GetBuildingName(),
                    SubDivisionNumber = action.GetSubDivisionNumber(),
                    StreetNo = action.GetStreetNo(),
                    Street = action.GetStreet(),
                    Suburb = action.GetSuburb(),
                    City = action.GetCity(),
                    Province = action.GetProvince(),
                    PostalCode = action.GetPostalCode()
                }
            };
            return deco;
        }
        public static string AsId(string clientId, params string[] values)
        {
            return $"{clientId}/{string.Join("/", values.Select(Parse))}";
        }
        public static string AsEntityId(string clientId, params string[] values)
        {
            return $"{clientId}/entity/{string.Join("/", values.Select(Parse))}";
        }
        public static string Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value.Trim().Replace(" ", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty).Replace("/", "-");
        }
        //public static void ViewEachTransformedActionLineItem(SiteData site, ActionItem action)
        //{

        //    var ActionRef = action.GetActionReference();
        //    var custRef = action.GetCustomerReference();
        //    var custName = action.GetCustomerName();
        //    var internalRef = action.GetInternalReference();
        //    var actionTypeName = action.GetActionTypeName();
        //    var actionTypeId = AsId(site.Id, action.GetActionTypeName());
        //    var direction = action.DetermineDirection();
        //    var instructions = action.GetInstructions();

        //    var sellToId = AsEntityId(site.Id, action.GetSellToReference());
        //    var sellToName = action.GetSellToName();
        //    var sellToRef = action.GetSellToReference();

        //    var measure = action.DetermineMeasure();

        //    var restriction = action.GetRestrictions();

        //    //if (actionId == "a" ) { throw new ArgumentNullException("customer"); }
        //    var shipToId = AsEntityId(site.Id, action.GetShipToReference());
        //    var shipToName = action.GetShipToName();
        //    var shipToRef = action.GetShipToReference();

        //    var mst = action.GetMaxStopTime();

        //    var decoId = AsId(site.Id, action.GetShipToReference());
        //    var decoToName = action.GetShipToName();
        //    var decoToRef = action.GetShipToReference();
        //    var Latitude = action.GetShipToLatitude();
        //    var Longitude = action.GetShipToLongitude();

        //    var UnitNo = action.GetUnitNo();
        //    var BuildingName = action.GetBuildingName();
        //    var StreetNo = action.GetStreetNo();
        //    var SubDivisionNumber = action.GetSubDivisionNumber();
        //    var Street = action.GetStreet();
        //    var Suburb = action.GetSuburb();
        //    var City = action.GetCity();
        //    var Province = action.GetProvince();
        //    var PostalCode = action.GetPostalCode();
        //    var MapCode = action.GetMapCode();

        //    var Weight = action.GetWeight();
        //    var Pieces = action.GetPieces();
        //    var Pallets = action.GetPallets();
        //    var VolumetricMass = action.GetVolumetricMass();
        //    var AmountEx = action.GetAmountEx();
        //    var AmountIncl = action.GetAmountIncl();
        //}

    }
}
