using System;
using Trackmatic.Rest.Core;

namespace CleanProfile
{
    public class Program
    {
        static void Main(string[] args)
        {

            var fromDate = new DateTime(2017, 09, 25, 00, 00, 00);
            var toDate = new DateTime(2017, 10, 01, 23, 59, 59);

            var site = new SiteData().SelpalSite();
            var api = CreateLogin(site);

            var routesToDelete = new CleanRoutes();
            routesToDelete.DeleteRoutes(api);

            var actionsToDelete = new CleanActions();
            actionsToDelete.DeleteActions(api, fromDate, toDate);

            var entitiesToDelete = new CleanEntities();
            entitiesToDelete.DeleteEntities(api);

            var decosToDelete = new CleanDecos();
            decosToDelete.DeleteDecos(api);
        }



        private static Api CreateLogin(SiteData site)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", site.Id, site.UserName);
            api.Authenticate(site.PassWord);
            return api;
        }
 
    }
}
