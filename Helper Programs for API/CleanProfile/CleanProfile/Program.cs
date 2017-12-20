using System;
using System.Collections.Generic;
using Trackmatic.Rest.Core;

namespace CleanProfile
{
    public class Program
    {
        static void Main(string[] args)
        {

            var fromDate = new DateTime(2017, 09, 25, 00, 00, 00);
            var toDate = new DateTime(2017, 10, 01, 23, 59, 59);

            var site = new SiteData().BidfoodWesternCape();
            var api = CreateLogin(site);

            var routesIdsToDelete = new List<string>
            {
                "422/b744bfd4-db27-4566-bc1f-682101f058c8/TFR",
                "422/1edbd3ef-0719-4f97-9a85-5622a5978af7/TFR",
                "422/bcb4adc1-2468-4975-a312-3a8decf03937/TFR",
                "422/4a4a606b-a400-4b79-9e00-9fd9017d116c/TFR",
                "422/3cf99a5e-c681-45cf-8718-9b2082944d42/TFR",
                "422/1decc5b5-0118-4ef9-83f4-200b11c1afa0/TFR",
                "422/5680e5f4-3157-4d11-b374-252200197ee8",
                "422/82df3c68-c139-421f-926d-37077e4ec214",
                "422/f726b0d0-9de3-4000-afa0-73418776fcee",
                "422/a1619741-f32c-4e22-b4b6-856c02eb5ad4/TFR",
                "422/c9aa3a9d-ea95-4657-ab17-66687fc3e372/TFR",
                "422/900c8fc2-8517-446e-9253-0650151763f6/TFR",
                "422/73fccbe3-419f-4f81-8c85-9d570927aec9",
                "422/2d45bf84-695b-49f6-b953-c28c05f101ec",
                "422/3044cf80-0efe-46b9-ace6-c18cd1c69b7e/TFR"
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
