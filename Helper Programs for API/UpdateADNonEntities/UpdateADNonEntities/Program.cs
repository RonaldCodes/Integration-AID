using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;
using UpdateADNonEntities.Csv;

namespace UpdateADNonEntities
{
    public class Program
    {
        static void Main(string[] args)
        {
            var api = CreateLogin("409");
            var csvReader = new CsvReader();
            var fileName = $@"Fixtures\Silveray Customer Email.csv";
            var content = File.ReadAllText(fileName);
            var line = csvReader.Read(content);
            var entities = line.Where(p => p.Data != null).Select(p => new EntityLines(p)).ToList();
            update(api, entities);
        }

        public static void update(Api api, List<EntityLines> entitiesFromFile)
        {
            var NotFound = new List<string>();

            foreach (var entityFromFile in entitiesFromFile)
            {
                var entityId = $"{api.Context.ClientId}/entity/{entityFromFile.GetRef().ToUpper()}";
                var existingEntity = api.ExecuteRequest(new LoadEntity(api.Context, entityId)).Data;
                if (existingEntity != null)
                {
                    var contacts = existingEntity.Contacts;
                    if(contacts.Count != 0)
                    {
                        foreach (var contact in contacts)
                        {
                            if (contact.Email == entityFromFile.Getemail())
                            {
                                contact.AdnConfiguration.Types = new List<string>()
                                    {
                                      "started",
                                      "delivery_confirmation"
                                    };
                                contact.AdnConfiguration.Email = true;
                                api.ExecuteRequest(new UpdateEntity(api.Context, existingEntity));
                                Console.WriteLine($@"Updated Contact: {existingEntity.Id}");
                            }
                            else
                            {
                                CreateNewADN(api, entityFromFile, existingEntity);
                            }
                        }
                    }
                    else
                    {
                        CreateNewADN(api, entityFromFile, existingEntity);
                    }
                }

                else
                {
                    NotFound.Add(entityFromFile.GetRef());
                }
            }
            File.AppendAllLines($@"Fixtures\NotFound.txt", NotFound);
            Console.ReadLine();
        }

        public static void CreateNewADN(Api api, EntityLines entityFromFile, Entity existingEntity)
        {
            var name = RemoveSpecialChars(entityFromFile.Getname());
            existingEntity.Contacts.Add(
            new EntityContact()
            {

                Id = $"409/{name}/{entityFromFile.GetRef()}",
                AdnConfiguration = new AdnConfiguration()
                {
                    Email = true,
                    Types = new List<string>()
                    {
                                      "started",
                                      "delivery_confirmation"
                    }
                },
                FirstName = entityFromFile.Getname(),
                LastName = "",
                Email = entityFromFile.Getemail()
            });
            api.ExecuteRequest(new UpdateEntity(api.Context, existingEntity));
            Console.WriteLine($@"Uploaded New Contact: {existingEntity.Id}");
        }
        public static string RemoveSpecialChars(string inputString)
        {
            var pattern = new Regex("[:!@#$%^&*()}{|\":?><\\;'/.,~ ]");
            var updatedString = pattern.Replace(inputString, "");
            return updatedString;
        }
        public static Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9210155014083");
            api.Authenticate("'P@ssword");
            return api;
        }
    }
}
