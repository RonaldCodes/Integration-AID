using DeleteActionsFromRouteBuilder.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;

namespace DeleteActionsFromRouteBuilder
{
   public class Program
    {
        static void Main(string[] args)
        {
            var site = new SiteData().TrackmaticTest();
            var api = Login(site);
            var actionsWithZones = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.SearchPlannedActionsWithZones(api.Context, "07BP", 1, 0, null, null)).Data.Data;
            var actionIds = actionsWithZones.Select(x => x.Id).Distinct().ToList();

            var locationIds = new List<string>();
            try
            {
                locationIds.AddRange(actionsWithZones.Select(p => p.Entity).Select(p => p.Deco.Id).Distinct().ToList());
            }
            catch (NullReferenceException)
            {

            }
            foreach (var locationId in locationIds)
            {
                // var deletelocation = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeleteLocation(api.Context, locationId));
            }
            foreach (var actionId in actionIds)
            {
               // var deleteAction = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeleteAction(api.Context, actionId2));
            }

        }

        private static Api Login(SiteData forSite)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", forSite.ClientId, forSite.UserName);
            api.Authenticate(forSite.PassWord);
            return api;
        }
    }
}
