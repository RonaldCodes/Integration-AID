using Agent.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using Trackmatic.Rest.Planning.Model;

namespace UploadActionsToRouteBuilder.Transformer
{
    public class RouteBuilderTransformer
    {
        private readonly SiteData _site;

        private readonly List<ActionItem> _actions;
        public RouteBuilderTransformer(SiteData site, List<ActionItem> actions)
        {
            _site = site;
            _actions = actions;
        }

        public List<Trackmatic.Rest.Planning.Model.Action> Actions()
        {
            var results = new List<Trackmatic.Rest.Planning.Model.Action>();

            foreach (var action in _actions)
            {
                results.Add(CreateRouteBuilderAction(_site, action));
               // ViewEachTransformedActionLineItem(site, action);
            }

            return results;

        }
        
        public static Trackmatic.Rest.Planning.Model.Action CreateRouteBuilderAction(SiteData _site, ActionItem action)
        {
            var routeBuilderAction = new Trackmatic.Rest.Planning.Model.Action
            {
                Id = AsId(_site.Id, action.GetActionReference()),
                Reference = action.GetActionReference(),
                CustomerReference = action.GetCustomerReference(),
                InternalReference = action.GetInternalReference(),
                ActionTypeId = AsId(action.GetActionTypeName()),
                ActionTypeName = action.GetActionTypeName(),
               // ExpectedDelivery = action.GetExpectedDeliveryDate(),
                ReceivedOn = DateTime.UtcNow,
                Direction = action.DetermineDirection(),
                ClientId = _site.Id,
                Instructions = action.GetInstructions(),
                //ZoneId = "",
                //ZoneName = "",
                SellTo = new SellTo
                    {
                         Id = AsEntityId(_site.Id,action.GetSellToReference()),
                         //IsAdhoc = false,
                         Name = action.GetSellToName(),
                         Reference = action.GetSellToReference(),
                    },
                Measure = action.DetermineMeasure(),
                Restrictions = new List<string>
                    {
                         action.GetRestrictions()
                    },
                Entity = new Entity
                {
                         Id = AsEntityId(_site.Id,action.GetShipToReference()),
                         Name = action.GetShipToName(),
                         Reference = action.GetShipToReference(),
                         Mst = action.GetMaxStopTime(),
                          Deco = new Deco
                        {
                             Id = AsId(_site.Id,action.GetShipToReference()),
                             Name = action.GetShipToName(),
                             Reference = action.GetShipToReference(),
                             Position =new List<double>{ action.GetShipToLongitude(), action.GetShipToLatitude()}.ToArray(),
                             Address = new Address
                            {
                                 UnitNo = action.GetUnitNo(),
                                 BuildingName = action.GetBuildingName(),
                                 StreetNo = action.GetStreetNo(),
                                 SubDivisionNumber = action.GetSubDivisionNumber(),
                                 Street = action.GetStreet(),
                                 Suburb = action.GetSuburb(),
                                 City = action.GetCity(),
                                 Province = action.GetProvince(),
                                 PostalCode = action.GetPostalCode(),
                                 MapCode = action.GetMapCode(),
                            }
                        }
                    },
                Metrics = new Trackmatic.Rest.Planning.Model.Metrics
                {
                        Weight = action.GetWeight(),
                        Pieces = action.GetPieces(),
                        Pallets = action.GetPallets(),
                        VolumetricMass = action.GetVolumetricMass(),
                        AmountEx = action.GetAmountEx(),
                        AmountIncl = action.GetAmountIncl(),
                    },
            };
            return routeBuilderAction;
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
        public static void ViewEachTransformedActionLineItem(SiteData site, ActionItem action)
        {

            var ActionRef = action.GetActionReference();
            var custRef = action.GetCustomerReference();
            var custName = action.GetCustomerName();
            var internalRef = action.GetInternalReference();
            var actionTypeName = action.GetActionTypeName();
            var actionTypeId = AsId(site.Id, action.GetActionTypeName());
            var direction = action.DetermineDirection();
            var instructions = action.GetInstructions();

            var sellToId = AsEntityId(site.Id, action.GetSellToReference());
            var sellToName = action.GetSellToName();
            var sellToRef = action.GetSellToReference();

            var measure = action.DetermineMeasure();

            var restriction = action.GetRestrictions();

            //if (actionId == "a" ) { throw new ArgumentNullException("customer"); }
            var shipToId = AsEntityId(site.Id, action.GetShipToReference());
            var shipToName = action.GetShipToName();
            var shipToRef = action.GetShipToReference();

            var mst = action.GetMaxStopTime();

            var decoId = AsId(site.Id, action.GetShipToReference());
            var decoToName = action.GetShipToName();
            var decoToRef = action.GetShipToReference();
            var Latitude = action.GetShipToLatitude();
            var Longitude = action.GetShipToLongitude();

            var UnitNo = action.GetUnitNo();
            var BuildingName = action.GetBuildingName();
            var StreetNo = action.GetStreetNo();
            var SubDivisionNumber = action.GetSubDivisionNumber();
            var Street = action.GetStreet();
            var Suburb = action.GetSuburb();
            var City = action.GetCity();
            var Province = action.GetProvince();
            var PostalCode = action.GetPostalCode();
            var MapCode = action.GetMapCode();

            var Weight = action.GetWeight();
            var Pieces = action.GetPieces();
            var Pallets = action.GetPallets();
            var VolumetricMass = action.GetVolumetricMass();
            var AmountEx = action.GetAmountEx();
            var AmountIncl = action.GetAmountIncl();
        }

        private readonly List<string> IllegalChars = new List<string>()
        {
            "+",
            "%",
            "!"
        };
    }
}
