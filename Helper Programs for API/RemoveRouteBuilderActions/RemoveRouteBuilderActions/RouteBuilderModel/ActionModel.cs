using System;
using System.Linq;
using Trackmatic.Rest.Core;

namespace RouteBuilderManipulator
{
    public class ActionWithLocation
    {
        public void DeletePlannedActioWithLocation(Api api, int take, int skip, DateTime startDate, DateTime endDate)
        {
            var actionsWithZones = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.SearchPlannedActionsWithZones(api.Context, "", take, skip, startDate, endDate)).Data.Data;
            var actionIds = actionsWithZones.Select(x => x.Id).Distinct().ToList();
            var locationIds = actionsWithZones.Select(p => p.Entity).Select(p => p.Deco.Id).Distinct().ToList();
            foreach (var locationId in locationIds)
            {
                var deletelocation = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeleteLocation(api.Context, locationId));
            }
            foreach (var actionId in actionIds)
            {
                var deleteAction = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeleteAction(api.Context, actionId));
            }
        }
    }
}
