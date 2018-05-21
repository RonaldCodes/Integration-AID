using UploadAddressesToAVT.Transformers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using UploadAddressesToAVT.Site;
using UploadAddressesToAVT.Csv;
using Trackmatic.Rest.Dit.Model;

namespace UploadAddressesToAVT
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvReader();
            var fileName = "Fixtures/trackmaticc2002277_1702309.xml";
            var content = File.ReadAllText(fileName);
            var line = csvReader.Read(content);
            var addresses = line.Where(p => p.Data != null).Select(p => new AddressLines(p)).ToList();
            UploadToAVT(addresses);
        }
        public static void UploadToAVT(List<AddressLines> addresses)
        {
            try
            {
                Console.WriteLine($"Uploading addresses to AVT.");
                var site = new SiteData().RTT();
                var avtTransformer = new AVTUploadModelTransformer(site,addresses);
                var avtModels = avtTransformer.Transform();
                var api = CreateLogin(site.ClientId);

                var batches = CreateBatches(avtModels);

                foreach (var batchList in batches)
                {
                    var upload = api.ExecuteRequest(new Trackmatic.Rest.Dit.Requests.UploadAddresses(api.Context, batchList));
                }

                // saveAvtAddressesToDb(trip);
                Console.WriteLine($"Uploaded order addresses to AVT successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not upload order addresses to AVT.");
                Console.WriteLine(e.ToJson(true));
                throw;
            }
        }

        public static List<List<DitAddress>> CreateBatches(List<DitAddress> address, int nSize = 100)
        {
            List<List<DitAddress>> batchAddressList = new List<List<DitAddress>>();
            for (int i = 0; i < address.Count; i += nSize)
            {
                batchAddressList.Add(address.GetRange(i, Math.Min(nSize, address.Count - i)));
            }
            return batchAddressList;
        }

        public static void DownloadFromAVT(DateTime from, DateTime to)
        {
            try
            {
                Console.WriteLine($"Uploading addresses to AVT.");
                var site = new SiteData().RTT();
                var api = CreateLogin(site.ClientId);

                var download = api.ExecuteRequest(new Trackmatic.Rest.Dit.Requests.GetAddresses(api.Context, null,from, to,Trackmatic.Rest.Dit.Model.EDitStatus.Validated, 500, 500));
                Console.WriteLine($"Uploaded order addresses to AVT successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not upload order addresses to AVT.");
                Console.WriteLine(e.ToJson(true));
                throw;
            }
        }

        private static Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }
    }
}
