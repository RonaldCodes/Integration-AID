using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;

namespace UpdateMst
{
    public class Program
    {
        static void Main(string[] args)
        {
            UpdateMsts("514");
        }

        public static void UpdateMsts(string _clientId)
        {
            var api = CreateLogin(_clientId);

            var batch = new BatchQuery<Entity>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadEntitiesInBatches(), EntityCallBack);
            batch.Execute();

            var batch2 = new BatchQuery<OLocation>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadLocationsInBatches(), DecoLookup);
            batch2.Execute();

        }

        public static Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }

        private static void DecoLookup(Api api, OLocation location)
        {
            location.DefaultStopTime = TimeSpan.FromMinutes(10);
            api.ExecuteRequest(new UpdateLocation(api.Context, location));
            Console.WriteLine($"Updated Location {location.Id}");
        }

        private static void EntityCallBack(Api api, Entity entity)
        {
            try
            {
                foreach (var deco in entity.Decos)
                {
                    deco.Mst = TimeSpan.FromMinutes(10);
                }
                api.ExecuteRequest(new UpdateEntity(api.Context, entity));
                Console.WriteLine($"Updated Entity{entity.Id}");
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
}
