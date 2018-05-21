using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _api = CreateLogin();
            var result =   GetExistingDecoCoordsFromCuro(_api, SafeJoin("/", "201", "CPOP001"));
            var result2 = GetExistingDecoCoordsFromCuro(_api, SafeJoin("/", "201", "CBRE001"));

            var result3 = GetExistingDecoCoordsFromCuro(_api, SafeJoin("/", "201", "entity", "CNAN018"));
        }

        private static Entity GetExistingEntityromCuro(Api api, string decoId)
        {
            try
            {
                if (!decoId.Contains("$tmp"))
                {
                    return api.ExecuteRequest(new LoadEntity(api.Context, decoId)).Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static OLocation GetExistingDecoCoordsFromCuro(Api api, string Id)
        {
            try
            {
                if (!Id.Contains("$tmp"))
                {
                    var decoId = api.ExecuteRequest(new LoadEntity(api.Context, Id)).Data.Decos.Select(x => x.DecoId).FirstOrDefault().ToString();
                    return api.ExecuteRequest(new LoadLocation(api.Context, decoId)).Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static string SafeJoin(string separator, params string[] items)
        {
            var toJoin = items.Where(p => !string.IsNullOrEmpty(p)).ToList();
            if (!toJoin.Any()) return string.Empty;
            return string.Join(separator, toJoin);
        }
        private static Api CreateLogin()
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", "201", "0000000000201");
            api.Authenticate("NvdQCnc7");
            return api;
        }
    }
}
