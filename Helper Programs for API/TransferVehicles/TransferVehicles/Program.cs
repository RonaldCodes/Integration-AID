using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;

namespace TransferVehicles
{
    public class Program
    {
        public static List<VehicleInfo> vehicles = new List<VehicleInfo>();
        static void Main(string[] args)
        {
            DownloadVehiclesFromSite("350");
        }
        private static void DownloadVehiclesFromSite(string fromSite)
        {
            var api = CreateLogin(fromSite);
            vehicles =  api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.LoadVehicles(api.Context)).Data;
        }
        private static void UploadVehiclesToNewSite(string toSite)
        {
            var api = CreateLogin(toSite);
            foreach (var vehicle in vehicles)
            {
                //vehicle.Client = toSite,
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
