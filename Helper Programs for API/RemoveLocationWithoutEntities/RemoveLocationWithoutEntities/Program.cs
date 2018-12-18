using Agent.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;

namespace RemoveLocationWithoutEntities
{
   public class Program
    {
        static void Main(string[] args)
        {
            var api = Login("9408065009082", "yase191!", "566");
            var take = 500;
            var skip2 = 1;
            var NumberOfPages = GetNumberOfPages(api, take, skip2);

            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                // WriteToFile(api, take, skip);
                RemoveLocations(api, take, skip);
            }
        }
        private static void WriteToFile(Api api, int take, int skip)
        {
            var entities = api.ExecuteRequest(new LoadEntities(api.Context, null, take, skip)).Data.Data;

            foreach (var entity in entities)
            {
               var locations = entity.Decos;
               if (locations.Count != 0)
                {
                    foreach (var location in locations)
                    {
                        Console.WriteLine($"{location.Id}");
                        var contents = $"{location.Id}{Environment.NewLine}";
                        var path = @"C:\Users\YaseenH\Desktop\EntitiesWithLocations2.csv";
                        File.AppendAllText(path, contents);
                    }
                }
            }
        }

        private static void RemoveLocations(Api api, int take, int skip)
        {
            var csvReader = new CsvReader();
            var path = @"./Fixtures/EntitiesWithLocations2.csv";
            var content = File.ReadAllText(path);
            var csvLines = csvReader.Read(content);
            var LocationIds = csvLines.Where(p => p.Data != null).Select(p => new LocationIds(p)).ToList().Select(x =>x.GetLocationId().ToString());

            var locations = api.ExecuteRequest(new LoadLocations(api.Context, null, take, skip)).Data.Data;

            foreach (var location in locations)
            {
                if (!LocationIds.Contains(location.Id) && location.Tags.Count == 0 && location.Reference != null)
                {
                    try
                    {
                        api.ExecuteRequest(new DeleteLocation(api.Context, "566/12012"));
                        Console.WriteLine($"Remove {location.Id}");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"{e.InnerException}");
                    }

                    //var contents = $"{location.Id}{Environment.NewLine}";
                    //var path2 = @"C:\Users\YaseenH\Desktop\LocationsToRemove2.csv";
                    //File.AppendAllText(path2, contents);
                }
            }
        }


        private static double GetNumberOfPages(Api api, int take, int skip)
        {
            return Math.Ceiling(((api.ExecuteRequest(new LoadLocations(api.Context, null, take, skip))).Data.Total) / take);
        }
        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
