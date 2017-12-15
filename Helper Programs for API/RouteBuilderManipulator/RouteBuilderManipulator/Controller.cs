using System;
using System.Collections.Generic;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Model;
using Trackmatic.Rest.Planning.Requests;

namespace RouteBuilderManipulator
{
  public class Controller
    {
        private int take = 500;
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        public void DeletePlannedActionWithLocation(SiteData forSite)
        {
            var api = Login(forSite);
            var deleteInstance = new ActionWithLocation();
           
            var NumberOfPages = GetNumberOfPages(api);
            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                deleteInstance.DeletePlannedActioWithLocation(api, take, skip, fromDate, toDate);
            }
        }

        public List<Trackmatic.Rest.Planning.Model.Action> GetPlannedActionsWithLocation(SiteData forSite)
        {
            var api = Login(forSite);
            var actionWithLocation = new ActionWithLocation();
            var actionList = new List<Trackmatic.Rest.Planning.Model.Action>();

            var NumberOfPages = GetNumberOfPages(api);
            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                actionList.AddRange(actionWithLocation.RetrivePlannedActions(api, take, skip, fromDate, toDate));
            }
            return actionList;
        }

        private double GetNumberOfPages(Api api)
        {
            return Math.Ceiling((api.ExecuteRequest(new SearchPlannedActionsWithZones(api.Context, null, 1, 0, fromDate, toDate)).Data.Total) / take);
        }

        public void UploadToSite(List<Trackmatic.Rest.Planning.Model.Action> planActions, SiteData toSite)
        {
            var api = Login(toSite);
            var upload = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.UploadActions(api.Context, new ActionCollection(planActions)));
        }

        public Trackmatic.Rest.Planning.Model.Action UpdateActionIds(Trackmatic.Rest.Planning.Model.Action action, SiteData fromSite, SiteData toSite)
        {
            // action.ActionTypeId = $"{toSite.ClientId}/{action.ActionTypeId.Split('/')[1]}";
            action.ActionTypeId = "";
            action.Id = action.Id.Replace(fromSite.ClientId, toSite.ClientId);
            action.ClientId = toSite.ClientId;
            action.Entity.Id = action.Entity.Id.Replace(fromSite.ClientId, toSite.ClientId);
            action.Entity.Deco.Id = action.Entity.Deco.Id.Replace(fromSite.ClientId, toSite.ClientId);
            return action;
        }

        private Api Login(SiteData forSite)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", forSite.ClientId, forSite.UserName);
            api.Authenticate(forSite.PassWord);
            return api;
        }
    }
}
