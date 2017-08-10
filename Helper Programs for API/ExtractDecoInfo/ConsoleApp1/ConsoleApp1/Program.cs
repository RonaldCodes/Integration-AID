using System;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;

namespace ExtractDitAddresses
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "KolokTest.csv"; //Write to a csv 
            var api = Login("9408065009082", "yase191!", "224");
            var take = 500;
            var skip2 = 1;
            var NumberOfPages = GetNumberOfPages(api, take, skip2);

            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                WriteToFile(api, fileName, take, skip);
            }

        }

        private static void WriteToFile(Api api, string fileName, int take, int skip)
        {
            var entities = api.ExecuteRequest(new LoadEntities(api.Context,null,take,skip)).Data.Data;

            foreach (var entity in entities)
            {
                var id = entity.Id;
                var entityReference = entity.Reference;
                var entityName = entity.Name.ToString();

                var decos = entity.Decos;
                var decoId = decos.First().Id;

                if (decoId == null)
                {
                    Console.WriteLine("Null found");
                }
                else if (decoId != null)
                {
                    var location = api.ExecuteRequest(new LoadLocation(api.Context, decos.First().DecoId)).Data;

                    var decoName = location.Name;
                    var decoShape = location.Shape.ToString();
                    var lon = location.Location.Longitude;
                    var lat = location.Location.Latitude;

                    var contents = $"{entityReference},{entityName},{decoName},{decoShape},{lon},{lat}{Environment.NewLine}";
                    var path = @"C:\Users\YaseenH\Desktop\Kolok2.csv";
                    File.AppendAllText(path, contents);
                }
            }
        }

        private static double GetNumberOfPages(Api api, int take, int skip)
        {
            return Math.Ceiling(((api.ExecuteRequest(new LoadEntities(api.Context, null, take, skip))).Data.Total) / take);
        }
        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
