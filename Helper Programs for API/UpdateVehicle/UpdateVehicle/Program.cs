using System;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;
using UpdateVehicle.csv;

namespace UpdateVehicle
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var filePath = @"Fixtures\vehicles.csv";
            var content = ReadContent(filePath);
            var csvReader = new CsvReader();
            var lines = csvReader.Read(content);
            var vehicleData = lines.Where(x => x != null).Select(x => new VehicleData(x)).ToList();

            foreach (var vehicle in vehicleData)
            {
                var reg = vehicle.GetRegistration();
                try
                {
                    var api = Login("9408065009082", "yase191!",vehicle.GetClientId());
                    var vehicleToUpdate = api.ExecuteRequest(new LoadVehicle(api.Context, vehicle.GetRegistration())).Data;
                    vehicleToUpdate.Class = new Trackmatic.Rest.Core.Model.VehicleClass()
                    {
                        Id = string.Join("/", vehicle.GetClientId(), Guid.NewGuid()),
                        Name = vehicle.GetVehicleClass(),
                        Reference = vehicle.GetVehicleClass(),
                        Type = vehicle.GetVehicleClass()
                    };

                    //var classOfVehicle = new Trackmatic.Rest.Core.Model.VehicleClass()
                    //{
                    //    Id = string.Join("/", vehicle.GetClientId(), Guid.NewGuid()),
                    //    Name = vehicle.GetVehicleClass(),
                    //    Reference = vehicle.GetVehicleClass(),
                    //    Type = vehicle.GetVehicleClass()
                    //};

                    vehicleToUpdate.Pallets = vehicle.GetPallets();
                    vehicleToUpdate.VolumetricMass = vehicle.GetVolume();
                    //vehicleToUpdate.Weight = vehicle.ConvertToKg();
                    api.ExecuteRequest(new SaveVehicle(api.Context, vehicleToUpdate));
                    Console.WriteLine("Done with: "+ reg);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.ToString());
                    Console.WriteLine(reg);
                    File.AppendAllLines(@"ExceptionTest\Errors.txt", new[] { vehicle.GetRegistration() });
                }
            }
            Console.ReadLine();


            Console.ReadLine();
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }

        public static string ReadContent(string filePath)
        {
            var file = filePath;
            return File.ReadAllText(file);
        }
    }
}
