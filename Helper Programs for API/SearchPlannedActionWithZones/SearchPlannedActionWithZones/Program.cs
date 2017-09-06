using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Requests;

namespace SearchPlannedActionWithZones
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = Login("9408065009082", "yase191!", "404");
            var take = 500;
            DateTime startDate = new DateTime(2017, 08, 01);
            DateTime endDate = new DateTime(2017, 09, 06);
            var NumberOfPages = GetNumberOfPages(api, take, startDate, endDate);
            var fileName = "Actions.csv";//Write to a csv
            var Header = $"Reference, Zone";
            File.AppendAllLines(fileName, new[] { Header });
            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                WriteToFile(api, fileName, take, skip, startDate, endDate);
            }
            Console.ReadLine();
        }

        private static void WriteToFile(Api api, string fileName, int take, int skip, DateTime startDate, DateTime endDate)
        {
            var actionsWithZones = api.ExecuteRequest(new SearchPlannedActionsWithZones(api.Context, "", take, skip, startDate, endDate)).Data.Data;


            foreach (var actionWithZones in actionsWithZones)
            {
                var data = $"{actionWithZones.CustomerReference},{actionWithZones.ZoneName},";
                File.AppendAllLines(fileName, new[] { data });
            }
        }

        private static double GetNumberOfPages(Api api, int take, DateTime startDate, DateTime endDate)
        {
            return Math.Ceiling((api.ExecuteRequest(new SearchPlannedActionsWithZones(api.Context, null, 1, 0, startDate, endDate)).Data.Total) / take);
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
