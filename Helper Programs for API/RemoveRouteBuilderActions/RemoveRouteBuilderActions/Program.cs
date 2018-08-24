using System;

namespace RouteBuilderManipulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var site = new SiteData();
            var controller = new Controller();
            controller.fromDate = new DateTime(2017, 01, 01);
            controller.toDate = new DateTime(2018, 08, 29);
            controller.forSite = site.TrackmaticEastLondon();
            controller.DeletePlannedActionWithLocation();

          Console.ReadLine();
        }
    }
}
