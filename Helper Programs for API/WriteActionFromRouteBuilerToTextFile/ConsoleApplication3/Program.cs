using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Requests;

namespace ConsoleApplication3
{
   public class Program
    {
        public static DateTime fromDate = new DateTime(2018, 03, 01);
        public static DateTime toDate = new DateTime(2018, 06, 30);
        public static int take = 500;
        static void Main(string[] args)
        {
            var site = new SiteData().LGB();
            var api = Login(site);
            


            var NumberOfPages = GetNumberOfPages(api);
            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                var actionsWithZones = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.SearchPlannedActionsWithZones(api.Context, "", take, skip, fromDate, toDate)).Data.Data;
                foreach (var action in actionsWithZones)
                {
                    if (action.Entity.Deco == null)
                    {
                        string createText = $"'{action.Reference}'," + Environment.NewLine;
                        File.AppendAllText($@"C:\Users\YaseenH\Desktop\actionIds.txt", createText);

                       // string readText = File.ReadAllText($@"C:\Users\YaseenH\Desktop\actionIds.txt");
                    }

                }
            }

        }
        private static Api Login(SiteData forSite)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", forSite.ClientId, forSite.UserName);
            api.Authenticate(forSite.PassWord);
            return api;
        }
        private static double GetNumberOfPages(Api api)
        {
            return Math.Ceiling((api.ExecuteRequest(new SearchPlannedActionsWithZones(api.Context, null, 1, 0, fromDate, toDate)).Data.Total) / take);
        }
    }
    public class SiteData
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public SiteData LGB()
        {
            return new SiteData()
            {
                ClientId = "407",
                UserName = "0000000000407",
                PassWord = "!!T8Zuh$"
            };
        }
    }
}
