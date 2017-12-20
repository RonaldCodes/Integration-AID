using System;
using System.Collections.Generic;
using System.IO;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;

namespace WriteRouteInfoToCSV
{
    public class Program
    {
        static void Main(string[] args)
        {
            var api = Login("9408065009082", "yase191!", "422");
            var routesToRetrieve = new List<string>
            {
                "422/ALT06/TFR",
                "422/ALT05/TFR",
                "422/ALT04/TFR",
                "422/ALT02/TFR",
                "422/ALT01/TFR"
            };

           GetRouteInfo(api, routesToRetrieve);
        }

        public static void GetRouteInfo(Api api, List <string> routesToRetrieve)
        {
            foreach (var route in routesToRetrieve)
            {
                try
                {
                    var testRoute = api.ExecuteRequest(new LoadRouteInstance(api.Context, route)).Data;
                    WriteToTextFile(testRoute);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.ToString()}...");
                }
            }
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }

        public static void WriteToTextFile(RouteInstance route)
        {
            var routeDecos = route.Route.RouteDecos;
            var routeName = route.Reference.Split('_')[0];

            var header = $"Route, Location Name, Location Reference, Order{Environment.NewLine}";
            var path = $@"C:\Users\YaseenH\Desktop\Adhoc\Bidfood\OurSeq{routeName}.csv";
            File.AppendAllText(path, header);

            foreach (var deco in routeDecos)
            {
                var contents = $"{routeName}, {deco.Deco.DisplayName}, {deco.Deco.Id.Split('/')[1]}, {deco.Order.ToString()}{Environment.NewLine}";
                File.AppendAllText(path, contents);
            }
        }
    }
}
