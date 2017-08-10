using ExtractDitAddresses.CSVReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Dit.Model;

namespace ExtractDitAddresses
{
    public class Program
    {
        public static List<ActionLine> _actionsLines = new List<ActionLine>();
        static void Main(string[] args)
        {

            var filePath = @"Data\Cori_June_Actions.csv";
            var content = ReadContent(filePath);
            var csvReader = new CsvReader();
            var lines = csvReader.Read(content);
            var actionLines = lines.Where(x => x != null).Select(x => new ActionLine(x)).ToList();
            _actionsLines = actionLines;
            //var superRoute = CreateRoute(RouteName);

            var api = Login("9408065009082","yase191!","206");

            var batch = new BatchQuery<DitAddress>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadDitAddressesInBatches(), Callback);
            batch.Execute();

            Console.ReadLine();
        }

        public static void Callback(Api api, DitAddress address)
        {
            var thing = _actionsLines.Where(x => x.GetInternalReference() == address.Reference).FirstOrDefault();
            if (thing != null)
            {
                var path = "Addresses.csv";
                var data = $"{address.Id},{address.Status},{address.Instructions},{address.Name},{address.Reference},{address.Warehouses},{address.Address},{address.Coordinates}";
                File.AppendAllLines(path,new[] { data });
            }
            else
            {
                var path = "Report.txt";
                var data = $"{thing.GetInternalReference()} Not found in DIT";
                File.AppendAllLines(path, new[] { data });
            }
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }

        public static string ReadContent(string filePath)
        {
            var file = filePath;
            return File.ReadAllText(file);
        }
    }
}
