using System;
using System.Collections.Generic;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using static FruitSpotAllEntities.Utility;

namespace FruitSpotAllEntities
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "Entities.csv";
            var api = Login("9408065009082", "yase191!", "157");
            var take = 500;
            var NumberOfPages = GetNumberOfPages(api,take);

            for (int i = 0; i<= NumberOfPages; i++)
            {
                var percentage = Math.Round(i*100/NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                WriteToFile(api, fileName, take, skip);
            }
            Console.ReadLine();
        }

        private static void WriteToFile(Api api, string fileName, int take , int skip)
        {
            var entities = api.ExecuteRequest(new LoadEntities(api.Context, null, take, skip)).Data.Data; ;

            foreach (var entity in entities)
            {
                var decos = entity.Decos;
                foreach (var deco in decos)
                {
                    var info = new List<FileLine>
                        {
                            Wrap(entity.Id,"Entity Id"),
                             Wrap(entity.Reference,"Reference"),
                              Wrap(entity.Name,"Name"),
                               Wrap(deco.DecoId,"Deco Id"),
                        };
                    Append(fileName, info);
                }
            }
        }
        private static double GetNumberOfPages(Api api, int take)
        {
          return Math.Ceiling((api.ExecuteRequest(new LoadEntities(api.Context, null, 1, 0)).Data.Total) / take);
        }
        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
