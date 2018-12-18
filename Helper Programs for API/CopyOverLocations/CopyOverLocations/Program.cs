using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;

namespace CopyOverLocations
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileName = "Test.csv"; //Write to a csv 
            var api = Login("9408065009082", "yase191!", "452");
            var take = 500;
            var skip2 = 1;
            var NumberOfPages = GetNumberOfPages(api, take, skip2);

            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                CopyLocations(api, fileName, take, skip);
            }
            Console.WriteLine($"Done...");
            Console.ReadLine();
        }

        private static void CopyLocations(Api api, string fileName, int take, int skip)
        {
            var locations = api.ExecuteRequest(new LoadLocations(api.Context,null, take, skip)).Data.Data;
            var api2 = Login("9408065009082", "yase191!", "566");
            foreach (var location in locations)
            {
                var newLocation = location;
                newLocation.Id = location.Id.Replace("452", "566");
                newLocation.ClientId = "566";
                var response = api2.ExecuteRequest(new SaveLocation(api2.Context, newLocation));
            }

            // var contents = $"{entityReference},{entityName},{decoName},{decoShape},{lon},{lat}{Environment.NewLine}";
            // var path = @"C:\Users\YaseenH\Desktop\Kolok2.csv";
            // File.AppendAllText(path, contents);
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
