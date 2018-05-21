using System;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Requests;

namespace RouteBuilderManipulator
{
  public class Controller
    {
        private int take = 500;
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public Api api { get; set; }
        public SiteData forSite { get; set; }

        public void DeletePlannedActionWithLocation()
        {
            api = Login();
            var deleteInstance = new ActionWithLocation();
           
            var NumberOfPages = GetNumberOfPages();
            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                deleteInstance.DeletePlannedActioWithLocation(api, take, skip, fromDate, toDate);
            }
        }

        public void WritePlannedActionWithLocation()
        {
            api = Login();
            var deleteInstance = new ActionWithLocation();

            var NumberOfPages = GetNumberOfPages();
            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                deleteInstance.DeletePlannedActioWithLocation(api, take, skip, fromDate, toDate);
            }
        }
        private double GetNumberOfPages()
        {
            return Math.Ceiling((api.ExecuteRequest(new SearchPlannedActionsWithZones(api.Context, null, 1, 0, fromDate, toDate)).Data.Total) / take);
        }
        private Api Login()
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", forSite.ClientId, forSite.UserName);
            api.Authenticate(forSite.PassWord);
            return api;
        }
    }
}
