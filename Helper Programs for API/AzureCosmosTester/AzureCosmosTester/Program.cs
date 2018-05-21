using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Threading.Tasks;

namespace AzureCosmosTester
{
    public class Program
    {
        private const string EndpointUrl = "https://transpharm.documents.azure.com:443/";
        private const string PrimaryKey = "cgRKbd7KKUpJjB9ij2Yrzt3tSO1NsaJBTOMoXsDTg9mhPcwun49mFKui3HfsLZNoKfHFBn8tc8Ph7Ud5AT4kXQ==";
        private readonly string databaseId = "transpharmRoutes";
        private readonly string collectionId = "transpharmRoutes_Col";
        private DocumentClient client;
        static void Main(string[] args)
        {
            try
            {
                Program p = new Program();
                p.GetStartedDemo().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }
        private async Task GetStartedDemo()
        {
            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseId), new DocumentCollection { Id = collectionId });

            Family andersenFamily = new Family
            {
                Id = "Andersen.2",
                LastName = "Andersen",
                Parents = new Parent[]
        {
                new Parent { FirstName = "Thomas" },
                new Parent { FirstName = "Mary Kay" }
        },
                Children = new Child[]
        {
                new Child
                {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Fluffy" }
                        }
                }
        },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = true
            };

            await this.CreateFamilyDocumentIfNotExists(databaseId, collectionId, andersenFamily);


            var test = await this.GetDocFromAzureDb(andersenFamily.Id);
            Console.WriteLine(test.Id);
            //Family wakefieldFamily = new Family
            //{
            //    Id = "Wakefield.7",
            //    LastName = "Wakefield",
            //    Parents = new Parent[]
            //        {
            //    new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
            //    new Parent { FamilyName = "Miller", FirstName = "Ben" }
            //        },
            //    Children = new Child[]
            //        {
            //    new Child
            //    {
            //            FamilyName = "Merriam",
            //            FirstName = "Jesse",
            //            Gender = "female",
            //            Grade = 8,
            //            Pets = new Pet[]
            //            {
            //                    new Pet { GivenName = "Goofy" },
            //                    new Pet { GivenName = "Shadow" }
            //            }
            //    },
            //    new Child
            //    {
            //            FamilyName = "Miller",
            //            FirstName = "Lisa",
            //            Gender = "female",
            //            Grade = 1
            //    }
            //        },
            //    Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
            //    IsRegistered = false
            //};

            //await this.CreateFamilyDocumentIfNotExists("FamilyDB", "FamilyCollection", wakefieldFamily);
        }
        private void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
        private async Task CreateFamilyDocumentIfNotExists(string databaseName, string collectionName, Family family)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, family.Id));
                await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, family.Id), family);
                this.WriteToConsoleAndPromptToContinue("Found {0}", family.Id);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), family);
                    this.WriteToConsoleAndPromptToContinue("Created Family {0}", family.Id);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<Family> GetDocFromAzureDb(string id)
        {
            try
            {
                return await this.client.ReadDocumentAsync<Family>(UriFactory.CreateDocumentUri(databaseId, collectionId, id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
            }
            return null;
        }
    }
    public class Family
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string LastName { get; set; }
        public Parent[] Parents { get; set; }
        public Child[] Children { get; set; }
        public Address Address { get; set; }
        public bool IsRegistered { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Parent
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
    }

    public class Child
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public int Grade { get; set; }
        public Pet[] Pets { get; set; }
    }

    public class Pet
    {
        public string GivenName { get; set; }
    }

    public class Address
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }
}
