using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;

namespace WriteLocationInfoToCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "BridgestoneLocations.csv"; //Write to a csv 
            var api = Login("9408065009082", "yase191!", "566");
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
            var locations = api.ExecuteRequest(new LoadLocations(api.Context,null, take, skip)).Data.Data;
            foreach (var location in locations)
            {
                if(location.Reference != null)
                {
                    var decoName = location.Name;
                    var decoRef = location.Reference.ToString();
                    var decoShape = location.Shape.ToString();
                    var lon = location.Location.Longitude;
                    var lat = location.Location.Latitude;

                    var contents = $"{decoRef},{decoName},{lon},{lat}{Environment.NewLine}";
                    var path = @"C:\Users\YaseenH\Desktop\TranspharmLocations.csv";
                    File.AppendAllText(path, contents);
                }
                Console.WriteLine($"{location.Name}Written...");
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
