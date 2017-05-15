using System;
using System.Collections.Generic;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;
using Trackmatic.Rest.Dit.Requests;
using static ExtractPersonnelfromApi.Utility;

namespace ExtractPersonnelfromApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "ZAF304.csv"; //Write to a csv 
            var api = Login("9408065009082", "yase191!", "304");
            var take = 500;
            DateTime startDate = new DateTime(2017, 01, 01);
            DateTime endDate = new DateTime(2017, 05, 15);
            var status = Trackmatic.Rest.Dit.Model.EDitStatus.New; // Type of status
            var NumberOfPages = GetNumberOfPages(api, take, startDate, endDate, status);

            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                WriteToFile(api, fileName, take, skip, startDate, endDate, status);
            }
            Console.ReadLine();
        }

        private static void WriteToFile(Api api, string fileName, int take, int skip, DateTime startDate, DateTime endDate, Trackmatic.Rest.Dit.Model.EDitStatus status)
        {
            var entries = api.ExecuteRequest(new GetAddresses(api.Context, "" ,startDate, endDate, status, take, skip)).Data.Data;

            foreach (var entry in entries)
            {
                //var street = entry.Address.Street.ToString().ToUpper();
                var code = entry.Address.PostalCode.ToUpper().Contains("ZAF");

                if (code == true)
                {
                    var id = entry.Id;
                    var stat = entry.Status.ToString();
                    var name = entry.Name.ToString();
                    var streetName = entry.Address.Street.ToString();

                    var info = new List<FileLine>
                           {
                                   Wrap(entry.ClientId, "ClientId"),
                                   Wrap(id, "Id"),       
                                   Wrap(stat, "First Name"),
                                   Wrap(name, "Name"),
                                   Wrap(streetName, "Street")                              

                            };
                Append(fileName, info);

                }
            }
        }

        private static double GetNumberOfPages(Api api, int take, DateTime startDate, DateTime endDate, Trackmatic.Rest.Dit.Model.EDitStatus status)
        {
            return Math.Ceiling((api.ExecuteRequest(new GetAddresses(api.Context, null, startDate, endDate, status, 1, 0)).Data.Total)/take);
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
