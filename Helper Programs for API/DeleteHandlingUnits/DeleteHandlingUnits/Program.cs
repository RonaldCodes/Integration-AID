using System.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;

namespace DeleteHandlingUnits
{
    public class Program
    {
        static void Main(string[] args)
        {
            var api = CreateLogin("110");
            var handlingunit = api.ExecuteRequest(new LoadHandlingUnits(api.Context, new[] { "110/unit/477594858" }.ToList())).Data.First().Allocations.First();
            var allocated = new AllocationHandlingUnit()
            {
                RouteId = handlingunit.RouteInstanceId,
                RouteReference = handlingunit.RouteReference,
                ActionId = handlingunit.ActionId,
                ActionReference = handlingunit.ActionReference,
                HandlingUnitIds = new[] { "110/unit/477594858" }.ToList()
            };
            var upload = api.ExecuteRequest(new Trackmatic.Rest.Routing.Requests.DeAllocateHandlingUnits(api.Context, allocated));
        }

        private static Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }
    }
}
