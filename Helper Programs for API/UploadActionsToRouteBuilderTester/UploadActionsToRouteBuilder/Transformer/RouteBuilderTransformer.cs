using System;
using System.Linq;
using Trackmatic.Rest.Core;
using System.Collections.Generic;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;
using Trackmatic.Rest.Planning.Model;

namespace UploadActionsToRouteBuilder.Transformer
{
    public class RouteBuilderTransformer
    {
        private readonly TrackmaticProfileData profileData;

        private readonly ActionItem action;
        public RouteBuilderTransformer(TrackmaticProfileData _profileData, ActionItem _action)
        {
            profileData = _profileData;
            action = _action;
        }

        public List<Trackmatic.Rest.Planning.Model.Action> Actions()
        {
            var results = new List<Trackmatic.Rest.Planning.Model.Action>();
            results.Add(CreateRouteBuilderAction());
            return results;
        }
        
        public Trackmatic.Rest.Planning.Model.Action CreateRouteBuilderAction()
        {
            var existingDeco = GetExistingDecoCoordsFromCuro(profileData.CreateLogin(), AsId(profileData.ClientId, action.ShipToAddressId));
            var routeBuilderAction = new Trackmatic.Rest.Planning.Model.Action
            {
                Id = AsId(profileData.ClientId, action.ActionReference),
                Reference = action.ActionReference,
                CustomerReference = action.CustomerReference,
                InternalReference = action.InternalReference,
                ActionTypeId = AsId(action.ActionTypeId),
                ActionTypeName = action.ActionTypeName,
                ExpectedDelivery = action.ExpectedDelivery,
                ReceivedOn = DateTime.UtcNow,
                Direction = action.Direction,
                ClientId = profileData.ClientId,
                Instructions = action.Instructions,
                Measure = action.Measure,
                Entity = new Entity
                {
                         Id = AsEntityId(profileData.ClientId, action.ShipToReference),
                         Name = action.ShipToName,
                         Reference = action.ShipToReference,
                         Mst = action.MaximumServiceTime,
                          Deco = new Deco
                        {
                             Id = AsId(profileData.ClientId, action.ShipToAddressId),
                             Name = action.ShipToAddressName,
                             Reference = action.ShipToReference,
                             Position =new List<double>
                             {
                                 (existingDeco != null) ? existingDeco.Entrance.Longitude : 0.0,
                                 (existingDeco != null) ? existingDeco.Entrance.Latitude : 0.0
                             }.ToArray(),
                             Address = new Trackmatic.Rest.Planning.Model.Address
                             {
                                 UnitNo = action.UnitNo,
                                 BuildingName = action.BuildingName,
                                 StreetNo = action.StreetNo,
                                 SubDivisionNumber = action.SubDivisionNumber,
                                 Street = action.Street,
                                 Suburb = action.Suburb,
                                 City = action.City,
                                 Province = action.Province,
                                 PostalCode = action.PostalCode,
                                 MapCode = action.MapCode,
                            }
                        }
                    },
                Metrics = new Trackmatic.Rest.Planning.Model.Metrics
                {
                    Weight = action.Weight,
                    Pieces = action.Pieces,
                    Pallets = action.Pallets,
                    VolumetricMass = action.VolumetricMass,
                    AmountEx = action.AmountEx,
                    AmountIncl = action.AmountIncl,
                },
            };
            return routeBuilderAction;
        }

        private static OLocation GetExistingDecoCoordsFromCuro(Api api, string decoId)
        {
            try
            {
                if (!decoId.Contains("$tmp"))
                {
                    return api.ExecuteRequest(new LoadLocation(api.Context, decoId)).Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string AsId(string clientId, params string[] values)
        {
            return $"{clientId}/{string.Join("/", values.Select(Parse))}";
        }
        public static string AsEntityId(string clientId, params string[] values)
        {
            return $"{clientId}/entity/{string.Join("/", values.Select(Parse))}";
        }
        private static string Parse(string value)
        {
            return value.Trim().Replace(' ', '_').Replace('-', '_').Replace('.', '_');
        }

        private readonly List<string> IllegalChars = new List<string>()
        {
            "+",
            "%",
            "!"
        };
    }
}
