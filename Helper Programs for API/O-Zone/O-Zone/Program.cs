using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;

namespace O_Zone
{
    class Program
    {
        private static Kml _kml;

        static void Main(string[] args)
        {
            AreaList wantedAreas = new AreaList();
            int count = 0;

            TextReader reader = File.OpenText("C:\\Users\\Yaseenh\\Desktop\\New folder\\Fibrehoods2.kml");
            KmlFile file = KmlFile.Load(reader);
            _kml = file.Root as Kml;

            var api = new Api("https://rest.trackmatic.co.za/api/v1", "110", "9408065009082");

            api.Authenticate("yase191!");

            if (_kml != null)
            {
                foreach (var polygon in _kml.Flatten().OfType<Polygon>())
                {
                    foreach (var area in wantedAreas.areas)
                    {
                        if (((Placemark)polygon.Parent).StyleUrl.OriginalString == "#fibrehoods_style")
                        {
                            var name = ((Placemark)polygon.Parent).Name;
                            if (area == name)
                            {
                                if (!name.Any(c => char.IsDigit(c)))
                                {
                                    count++;
                                    Console.WriteLine(name + "\n");

                                    var coorD = polygon.OuterBoundary.LinearRing.Coordinates;
                                    var Coords = new List<OCoord>();
                                    foreach (var item in coorD)
                                    {
                                        var latitude = item.Latitude;
                                        var longitude = item.Longitude;
                                        var coordinates = string.Format("{0},{1},{2}", "", longitude, latitude);
                                        Console.WriteLine(coordinates);

                                        var testcor = new OCoord();
                                        testcor.Longitude = longitude;
                                        testcor.Latitude = latitude;
                                        testcor.Radius = 0;
                                        Coords.Add(testcor);
                                    }

                                    var z = new OZone
                                    {
                                        ClientId = "110",
                                        Name = name,
                                        Coords = Coords,
                                        Id = $"110/{Guid.NewGuid()}",
                                        Shape = EZoneShape.Polygon,
                                        Type = EOZoneType.Stay
                                    };
                                    api.ExecuteRequest(new SaveZone(api.Context, z));
                                    //break;

                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Total Entries :"+count);
            Console.ReadLine();
        }
    }
}
