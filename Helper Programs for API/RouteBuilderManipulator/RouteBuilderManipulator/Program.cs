using System;

namespace RouteBuilderManipulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var site = new SiteData();
            var trackmaticTestSite = site.TrackmaticTest();

            var controller = new Controller();
            controller.fromDate = new DateTime(2017, 01, 01);
            controller.toDate = new DateTime(2017, 10, 29);
            controller.forSite = trackmaticTestSite;
            controller.DeletePlannedActionWithLocation();

            Console.ReadLine();
        }
    }
}
