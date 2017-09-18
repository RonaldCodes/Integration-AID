using Agent.Csv;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Model;
using UploadActionsToRouteBuilder.Transformer;

namespace UploadActionsToRouteBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvReader();

            var path = @"TestFiles/TestFileTemplate3.csv";

            var content = File.ReadAllText(path);
            var line = csvReader.Read(content);

            var actions = line.Where(p => p.Data != null).Select(p => new ActionItem(p)).ToList();
            var site = TrackmaticTestSite();
            var routeBuilderTransformer = new RouteBuilderTransformer(site, actions);
            var planActions = routeBuilderTransformer.Actions();
            var api = CreateLogin(TrackmaticTestSite());
            var upload = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.UploadActions(api.Context,new ActionCollection(planActions)));
        }

        private static Api CreateLogin(SiteData site)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", site.Id, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }
        public static SiteData TrackmaticTestSite()
        {
            return new SiteData()
            {
                Id = "110",
            };

        }
    }
}
