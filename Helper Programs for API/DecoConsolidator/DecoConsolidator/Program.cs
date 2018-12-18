using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Requests;

namespace DecoConsolidator
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsolidateEntitiesIfItDoesNotExist();
        }
        public static void ConsolodateLocations()
        {
            Console.WriteLine($"Checking locations.");
            var api = CreateLogin("186");

            var existingLocation = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.LoadLocationsById(api.Context, new List<string>() { "" })).Data;
                if (existingLocation.Count == 0)
                {
                    //var upload = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.SaveLocation(api.Context, locationModel));
                    //Console.WriteLine($"Uploaded new location {locationModel.Id} successfully.");
                }
                else
                {
                    //Console.WriteLine($"Did not upload location {locationModel.Id}.");
                }
            
        }

        public static void ConsolidateEntitiesIfItDoesNotExist()
        {
            Console.WriteLine($"Checking entities.");
            var api = CreateLogin("186");

                var existingEntity = api.ExecuteRequest(new LoadEntity(api.Context, "186/entity/101476SC0001")).Data;
                if (existingEntity == null)
                {
                    //var upload = api.ExecuteRequest(new SaveEntity(api.Context, entityModel));
                    //Console.WriteLine($"Uploaded new entity {entityModel.Id} successfully.");
                }
                else
                {
                   // Console.WriteLine($"Did not upload entity {entityModel.Id}.");
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
