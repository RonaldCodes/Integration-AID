using System;
using System.IO;
using System.Linq;
using System.Text;
using SharpKml.Dom;
using SharpKml.Engine;

namespace KmlReader
{
    class KMLExtractor
    {
        private static Kml _kml;

        static void Main(string[] args)
        {

            TextReader reader = File.OpenText("C:\\Users\\Yaseenh\\Desktop\\New folder\\Fibrehoods2.kml");
            KmlFile file = KmlFile.Load(reader);
            _kml = file.Root as Kml;

            var csv = new StringBuilder();

            if (_kml != null)
            {

                var Headings = string.Format("{0},{1},{2}", "Name\t\t", "Longitude", "Latitude");
                csv.AppendLine(Headings);

                //foreach (var placemark in _kml.Flatten().OfType<Placemark>())
                //{

                int count = 0;
                foreach (var polygon in _kml.Flatten().OfType<Polygon>())
                {
              
                    var name = ((Placemark)polygon.Parent).Name;
                    Console.WriteLine(name + "\n");
                    csv.Append(name);
                    csv.Append("\n");
                    count++;

                    var coorD = polygon.OuterBoundary.LinearRing.Coordinates;
                    foreach (var item in coorD)
                    {
                        var latitude = item.Latitude;
                        var longitude = item.Longitude;
                        var coordinates = string.Format("{0},{1},{2}", "", longitude, latitude);
                        Console.WriteLine(coordinates);
                        csv.AppendLine(coordinates);
                        
                    }
                    File.WriteAllText("C:\\Users\\Yaseenh\\Desktop\\New folder\\KMLExtraction.csv", csv.ToString());
                    break; //Take out to get all coordinates
                }
                Console.WriteLine(count);
            }
            else
            {
                Console.WriteLine("File is empty");
                csv.AppendLine("File is empty");
                File.WriteAllText("C:\\Users\\Yaseenh\\Desktop\\New folder\\KMLExtraction.csv", csv.ToString());
            }
        }
    }
}
 
