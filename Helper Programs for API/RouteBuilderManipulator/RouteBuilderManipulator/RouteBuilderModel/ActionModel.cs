using System;
using System.Collections.Generic;
using System.Linq;
using Trackmatic.Rest.Core;

namespace RouteBuilderManipulator
{
    public class ActionWithLocation
    {
        public void DeletePlannedActioWithLocation(Api api, int take, int skip, DateTime startDate, DateTime endDate)
        {
            var date = new DateTime(2018, 04, 16);

            var actionsWithZones = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.SearchPlannedActionsWithZones(api.Context, "", take, skip, startDate, endDate)).Data.Data;
            var test = actionsWithZones.Where(x => x.ExpectedDelivery < date);
            var actionIds = test.Select(x => x.Id).Distinct().ToList();


            var locationIds = new List<string>();
            try
            {
                 locationIds.AddRange(test.Select(p => p.Entity).Select(p => p.Deco.Id).Distinct().ToList());
            }
            catch (NullReferenceException)
            {
                
            }

            foreach (var locationId in locationIds)
            {
                var deletelocation = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeleteLocation(api.Context, locationId));
            }
            foreach (var actionId in actionIds)
            {
                var deleteAction = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeleteAction(api.Context, actionId));
            }
        }

        public List<Trackmatic.Rest.Planning.Model.Action> RetrivePlannedActions(Api api, int take, int skip, DateTime startDate, DateTime endDate)
        {
            var actionsWithZones = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.SearchPlannedActionsWithZones(api.Context, "", take, skip, startDate, endDate)).Data.Data.ToList();
            return actionsWithZones;
        }
    }
}
