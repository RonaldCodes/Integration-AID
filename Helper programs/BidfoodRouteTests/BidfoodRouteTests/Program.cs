using Agent.Csv;
using BidfoodRouteTests.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace BidfoodRouteTests
{
    class Program
    {
        
        public static Api _api = CreateLogin("203");
        private static readonly string TemplateId = "382/ecfdebfb-9fd6-4b02-afed-e777fb3b2c38";

        static void Main(string[] args)
        {
            var filesLocation = @"C:\Users\YaseenH\Desktop\BidfoodRouteTests\BidfoodRouteTests\FileToProcess\";
            var model = CreateUploadModel(filesLocation);
         
            Console.WriteLine("Done");
        }

        public static UploadModel CreateUploadModel(string filesLocation)
        {
            var model = new UploadModel();

            string[] fileList = Directory.GetFiles(filesLocation, "*.csv");

            foreach (var file in fileList)
            {
                string PathName = file.ToString();
                string RouteName =(PathName.Substring(PathName.LastIndexOf("\\") + 1).Replace(".csv", "")) ;
                Console.WriteLine($"Reading file: {RouteName}");
                var superList = new List<string>();
                var content = File.ReadAllText(file.ToString());

                if (content != null)
                {
                    var csvReader = new CsvReader();
                    var lines = csvReader.Read(content);
                    var routeLines = lines.Where(x => x != null).Select(x => new RouteLine(x)).ToList();
                    var superRoute = CreateRoute(RouteName);
                    model.Route = superRoute;

                    foreach (var route in routeLines)
                    {
                        var deco = CreateDeco(route);
                        var entity = LookupEntity(route.GetAccNumber());
                        var action = CreateAction();

                        if (entity != null && deco.Id != null)
                        {
                            if (!model.Decos.Contains(deco))
                            {
                                model.Decos.Add(deco);
                                model.Entities.Add(entity);
                                model.Actions.Add(action);

                                var relationship = new Relationship();
                                relationship.DecoId = deco.Id;
                                relationship.EntityId = entity.Id;
                                relationship.ActionId = action.Id;

                                model.Add(relationship);
                            }
                        }
                        else
                        {
                            superList.Add($"Entity Id: { route.GetAccNumber()} doesn't exist has or no ship-to location");
                        }
                    }
                }

                WriteToTextFile(superList,PathName,RouteName);
                CreateLatLonCsv(model, PathName, RouteName);
                var api = CreateLogin("382");
                var upload = api.ExecuteRequest(new Upload(api.Context, model));
            }
            return model;
        }

        public static void CreateLatLonCsv(UploadModel model, string PathName, string RouteName)
        {
            var index = 0;
            var headers = $"Id,Location Name,Sequence,Route Name,Latitude,Longtitude{Environment.NewLine}";
            File.AppendAllText(PathName, headers);
            foreach (var deco in model.Decos)
            {
                index++;
                var data = $"{deco.Id.Split('/')[1]},{deco.Name},{index},{RouteName},{deco.Location.Latitude},{deco.Location.Longitude}{Environment.NewLine}";
                File.AppendAllText(PathName, data);
            }
        }

        public static void WriteToTextFile(List<string> superList, string pathName, string RouteName)
        {
            var pathText = pathName.Replace(".csv", ".txt");
           using (StreamWriter file = new StreamWriter(pathText))
            {
                foreach (var item in superList)
                {
                    file.WriteLine(item);
                }
            }
        }

        public static RouteModel CreateRoute(string RouteName)
        {
            return new RouteModel()
            {
                Id = string.Join("/", "382", Guid.NewGuid()),
                TemplateId = TemplateId,
                Reference = RouteName,
                StartDate = DateTime.Now,
                Options = new RouteOptions()
                {
                    Lock = new LockOptions()
                    {
                        Start = true,
                        End = true,
                        All = true
                    }
                }
            };
        }

        public static Action CreateAction()
        {
            var Id = Guid.NewGuid().ToString();
            return new Action()
            {
                Id = string.Join("/", 382, Id),
                Reference = Id,
                ClientId = "382"
            };
        }

        public static OLocation CreateDeco(RouteLine routeLine)
        {
            var entity = LookupEntity(routeLine.GetAccNumber());

            if (entity == null || entity.Decos.Count == 0)
            {
                return new OLocation()
                {
                    Id = null,
                    Reference = null
                };
            }
            var decoId = entity.Decos.Select(x => x.DecoId).FirstOrDefault();
            var deco = _api.ExecuteRequest(new LoadLocation(_api.Context, decoId)).Data;
            deco.Id = ReplaceId(deco.Id);
            deco.ClientId = "382";
            return deco;
        }

        //public static string ReadContent()
        //{
        //    var file = @"TestData\SUNNINGHILL TEST ROUTE 1.csv";
        //    return File.ReadAllText(file);
        //}

        public static Entity LookupEntity(string entityId)
        {
            var trackmaticEntityId = CreateEntityId(entityId);
            var entity = _api.ExecuteRequest(new LoadEntity(_api.Context, trackmaticEntityId)).Data;
            if (entity != null)
            {
                entity.Id = ReplaceId(entity.Id);
                return entity;
            }
            return new Entity()
            {
                Id = null,
                Reference = null
            };
        }

        public static string CreateEntityId(string Id)
        {
            return string.Join("/", "203", "entity", Id);
        }

        public static Api CreateLogin(string CustomerId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", CustomerId, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }

        public static string ReplaceId(string Id)
        {
            var newId = "382";
            var removedId = Id.Remove(0,3);
            var final = newId + removedId;
            return final;
        }
    }
}
