using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;

namespace WriteVehicleToCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = Login("9408065009082", "yase191!", "252");

            GetVehicleInfo(api);
        }

        public static void GetVehicleInfo(Api api)
        {
            try
            {
                var vehicles = api.ExecuteRequest(new LoadVehicles(api.Context)).Data;
                WriteToTextFile(vehicles);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.ToString()}...");
            }
        }

        public static void WriteToTextFile(List<VehicleInfo> vehicles)
        {

            var header = $"Registration, FleetNumber, Make , model{Environment.NewLine}";
            var path = $@"C:\Users\YaseenH\Desktop\Adhoc\Vehicles.csv";
            File.AppendAllText(path, header);

            foreach (var vehicle in vehicles)
            {
                var type = vehicle.Description;
                var contents = $"{vehicle.RegistrationNumber}, {vehicle.CurrentCallSign}, {vehicle.Make}, {vehicle.Model}{Environment.NewLine}";
                File.AppendAllText(path, contents);
            }
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
