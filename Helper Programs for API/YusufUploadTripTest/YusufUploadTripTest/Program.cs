using System.IO;
using System.Runtime.Serialization;
using YusufUploadTripTest.BuildersWebService;

class Program
{
    static void Main(string[] args)
    {
        using (var client = new IntegrationBuildersClient())
        {
            var serializer = new DataContractSerializer(typeof(Trip));
            var trip = (Trip)serializer.ReadObject(File.OpenRead("TestFiles/BWH_Edenvale_20170927_BV68LCGP_334147_V2.xml"));
            client.UploadTrip(trip);
        }

    }
}
