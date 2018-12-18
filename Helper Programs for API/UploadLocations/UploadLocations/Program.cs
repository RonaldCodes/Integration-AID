using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Trackmatic.Rest.Core;
using UploadLocations.Csv;
using UploadLocations.Site;
using UploadLocations.Transformers;
using Trackmatic.Rest.Routing.Requests;
using System.Linq;
using Trackmatic.Rest.Routing.Model;

namespace UploadLocations
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"./Fixtures/NewRouteCust.xlsx";
            //var csvReader = new CsvReader();
            //var content = File.ReadAllText(fileName);
            //var line = csvReader.Read(content);
            //var locations = line.Where(p => p.Data != null).Select(p => new LocationLines(p)).ToList();
            // UploadLocations(locations);

            var stream = File.OpenRead(fileName);
            var reader = new ExcelReader(stream);
            var data = reader.ReadFile();
            //UploadLocationsIfItDoesNotExist(data);
            UploadEntitiesIfItDoesNotExist(data);
            Console.WriteLine($"Done");
        }
        public static void UploadLocations(List<LocationLines> locations)
        {
            try
            {
                Console.WriteLine($"Uploading contacts.");
                var site = new SiteData().TranspharmTest();
                var locationTransformer = new LocationModelTransformer(locations, site);
                var locationModels = locationTransformer.Transform();
                var api = CreateLogin(site.ClientId);

                foreach (var locationModel in locationModels)
                {
                    var existingLocation = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.LoadLocationsById(api.Context, new List<string>() { locationModel.Id })).Data;
                    if (existingLocation.Count == 0)
                    {
                        var upload = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.SaveLocation(api.Context, locationModel));
                        Console.WriteLine($"Uploaded new location {locationModel.Id} successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Did not upload location {locationModel.Id}.");
                    }
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

        public static void UploadLocationsIfItDoesNotExist(List<ExcelModel> locations)
        {
            Console.WriteLine($"Checking locations.");
            var site = new SiteData().TranspharmPta();
            var locationTransformer = new ExcelLocationModelTransformer(locations, site);
            var locationModels = locationTransformer.Transform();
            var api = CreateLogin(site.ClientId);

            foreach (var locationModel in locationModels)
            {
                var existingLocation = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.LoadLocationsById(api.Context, new List<string>() { locationModel.Id })).Data;
                if (existingLocation.Count == 0)
                {
                        try
                        {
                            var upload = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.SaveLocation(api.Context, locationModel));
                            Console.WriteLine($"Uploaded new location {locationModel.Id} successfully.");
                        }
                        catch (Exception e)
                        {

                        }
                }
                else
                {
                    //try
                    //{
                    //    var upload = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.UpdateLocation(api.Context, locationModel));
                    //    Console.WriteLine($"Uploaded new location {locationModel.Id} successfully.");
                    //}
                    //catch (Exception e)
                    //{

                    //}
                    Console.WriteLine($"Did not upload location {locationModel.Id}.");
                }
            }
        }

        public static void UploadEntitiesIfItDoesNotExist(List<ExcelModel> locations)
        {
            Console.WriteLine($"Checking entities.");
            var site = new SiteData().TranspharmPta();
            var entityTransformer = new ExcelEntityModelTransformer(locations, site);
            var entityModels = entityTransformer.Transform();
            var api = CreateLogin(site.ClientId);
            foreach (var entityModel in entityModels)
            {
                try
                {
                    var existingEntity = api.ExecuteRequest(new LoadEntity(api.Context, entityModel.Id)).Data;
                    if (existingEntity == null)
                    {
                        //var upload = api.ExecuteRequest(new SaveEntity(api.Context, entityModel));
                        Console.WriteLine($"Uploaded new entity {entityModel.Id} successfully.");
                    }
                    else
                    {
                        var test = existingEntity;
                        foreach (var tag in entityModel.Tags)
                        {
                        }
                        //existingEntity.Tags = entityModel.Tags;
                        var upload = api.ExecuteRequest(new Trackmatic.Rest.Routing.Requests.UpdateEntity(api.Context, test));
                        Console.WriteLine($"Did not upload entity {entityModel.Id}.");
                    }
                }
                catch (Exception e)
                {

                }
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
