using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using UploadLocations.Csv;
using UploadLocations.Site;
using UploadLocations.Transformers;

namespace UploadLocations
{
    public class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvReader();
            var fileName = "Fixtures/trackmaticc2002277_1702309.xml";
            var content = File.ReadAllText(fileName);
            var line = csvReader.Read(content);
            var contacts = line.Where(p => p.Data != null).Select(p => new LocationLines(p)).ToList();
        }
        public static void UploadContacts(List<LocationLines> locations)
        {
            try
            {
                Console.WriteLine($"Uploading contacts.");
                var site = new SiteData().CityLogistics();
                var locationTransformer = new LocationModelTransformer(locations, site);
                var locationModels = locationTransformer.Transform();
                var api = CreateLogin(site.ClientId);

                foreach (var locationModel in locationModels)
                {
                    var upload = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.SaveLocation(api.Context, locationModel));
                    Console.WriteLine($"Uploaded location {locationModel.Name} successfully.");
                }

                Console.WriteLine($"All locations uploaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not upload location.");
                Console.WriteLine(e.ToJson(true));
                throw;
            }
        }

        private static Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }
    }
}
