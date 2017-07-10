using ExtractDitAddresses.CSVReader;
using System;
using System.Collections.Generic;
using System.IO;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace ExtractBatchActions
{
    public class Program
    {
        public static List<ActionLine> _actionsLines = new List<ActionLine>();
        static void Main(string[] args)
        {
            var clientId = "244";
            var api = Login("9408065009082", "yase191!", clientId);

            var start = new DateTime(2017,07,07,00,00,00);
            var end = new DateTime(2017, 07, 07, 23, 59, 59);

            var batch = new BatchQuery<Action>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadActionsInBatches(start,end), WriteToFile);
            batch.Execute();

            Console.ReadLine();
        }

        public static void WriteToFile(Api api, Action action)
        {
                var fileName = "Actions.csv";
                var data = $"{action.Id},{action.Reference},{action.Nature},{action.CustomerReference},{action.ActionTypeId},{action.Status}," +
                    $"{action.RouteId},{action.CreatedOn},{action.ReceivedOn},{action.SellTo},{action.ShipTo},{action.ShipTo.Address}";
                File.AppendAllLines(fileName, new[] { data });
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
