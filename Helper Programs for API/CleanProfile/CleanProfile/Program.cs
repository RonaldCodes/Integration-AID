using System;
using System.Collections.Generic;
using Trackmatic.Rest.Core;

namespace CleanProfile
{
    public class Program
    {
        static void Main(string[] args)
        {

            var fromDate = new DateTime(2018, 01, 13, 00, 00, 00);
            var toDate = new DateTime(2018, 01, 01, 13, 59, 59);

            var site = new SiteData().RichProducts();
            var api = CreateLogin(site);

            var routesIdsToDelete = new List<string>
            {
                "439/92703_2018_1_13",
                "439/92695_2018_1_13",
                "439/92697_2018_1_13",
                "439/92698_2018_1_13",
                "439/92704_2018_1_13",
                "439/92707_2018_1_13",
                "439/92690_2018_1_13",
                "439/92691_2018_1_13",
                "439/92692_2018_1_13",
                "439/92702_2018_1_13",
                "439/92680_2018_1_13",
                "439/92682_2018_1_13",
                "439/92689_2018_1_13",
                "439/92693_2018_1_13",
                "439/92694_2018_1_13",
                "439/92701_2018_1_13",
                "439/92696_2018_1_13",
                "439/92705_2018_1_13",
                "439/92706_2018_1_13",
                "439/92708_2018_1_13",
                "439/92709_2018_1_13",
                "439/92712_2018_1_13",
                "439/92713_2018_1_13",
                "439/92714_2018_1_13",
                "439/92681_2018_1_13",
                "439/92710_2018_1_13",
                "439/92711_2018_1_13",
                "439/92700_2018_1_13"

            };

            var routesToDelete = new CleanRoutes();
            routesToDelete.DeleteRoutesById(api, routesIdsToDelete);

            //var actionsToDelete = new CleanActions();
            //actionsToDelete.DeleteActions(api, fromDate, toDate);

            //var entitiesToDelete = new CleanEntities();
            //entitiesToDelete.DeleteEntities(api);

            //var decosToDelete = new CleanDecos();
            //decosToDelete.DeleteDecos(api);
        }



        private static Api CreateLogin(SiteData site)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", site.Id, site.UserName);
            api.Authenticate(site.PassWord);
            return api;
        }
 
    }
}
