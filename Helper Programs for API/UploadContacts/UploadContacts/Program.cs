using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using UploadContacts.Csv;
using UploadContacts.Site;
using UploadContacts.Transformers;

namespace UploadContacts
{
    public class Program
    {
        static void Main(string[] args)
        { 
            var csvReader = new CsvReader();
            var fileName = $@"Fixtures\UsersEastCluster.csv";
            var content = File.ReadAllText(fileName);
            var line = csvReader.Read(content);
            var contacts = line.Where(p => p.Data != null).Select(p => new ContactLines(p)).ToList();
            UploadContacts(contacts);
        }

        public static void UploadContacts(List<ContactLines> contacts)
        {
            try
            {
                Console.WriteLine($"Uploading contacts.");
                var site = new SiteData().BWH_Western_Region_Cluster();
                var contactTransformer = new ContactModelTransformer(contacts);
                var contactModels = contactTransformer.Transform();
                var api = CreateLogin(site.ClientId);

                foreach (var contactModel in contactModels)
                {
                    var upload = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.SaveContact(api.Context, contactModel));
                    Console.WriteLine($"Uploaded contact {contactModel.FirstName} successfully.");
                }

                Console.WriteLine($"All contacts uploaded contacts successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not upload contact.");
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
