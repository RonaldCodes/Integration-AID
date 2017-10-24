using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Requests;

namespace GetZoneFromRouteBuilder
{
   public class Program
    {
        static void Main(string[] args)
        {
            var api = Login("00000000000404", "tb!AEs8B", "404");
            var csvReader = new CsvReader();
            var path = @"Files/Book4.csv";
            var content = File.ReadAllText(path);
            var line = csvReader.Read(content);
            var coords = line.Where(p => p.Data != null).Select(p => new CoordsFile(p)).ToList();
            var Header = $"Store ID, Store GPS, Zone";
            File.AppendAllLines("Zones.csv", new[] { Header });

            foreach (var coord in coords)
            {
                var storeID = coord.GetCoord1();
                var position = coord.GetCoord2();
                var latitude = position.Split(',')[0];
                var longitude = position.Split(',')[1];

                var Zones = api.ExecuteRequest(new LoadZonesByLatLon(api.Context, double.Parse(latitude, System.Globalization.CultureInfo.InvariantCulture), double.Parse(longitude, System.Globalization.CultureInfo.InvariantCulture))).Data;
                foreach (var zone in Zones)
                {
                    var data = $"{storeID},{position},{zone.Name}";
                    File.AppendAllLines("Zones.csv", new[] { data });
                }
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
