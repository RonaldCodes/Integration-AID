using Agent.Csv;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using UploadActionsToRouteBuilder.Transformer;
using Trackmatic.Rest.Routing.Requests;
using Trackmatic.Api.Agent.Pipelines.Elements;

namespace UploadActionsToRouteBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var csvReader = new CsvReader();

            var path = @"TestFiles/TestJD.csv";

            var content = File.ReadAllText(path);
            var line = csvReader.Read(content);

            var actions = line.Where(p => p.Data != null).Select(p => new ActionItem(p)).ToList();
            var site = JDGroupite();
            var routeBuilderTransformer = new RoutesTransformer(site, actions);
            var upload = routeBuilderTransformer.CreateModel();
            var api = CreateLogin(site);
                try
                {
                    var element = new UploadElement(site.Id, upload);
                    api.ExecuteRequest(new Upload(api.Context, upload));
                }
                catch 
                {
                    throw ;
                }
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
        public static SiteData JDGroupite()
        {
            return new SiteData()
            {
                Id = "417",
            };
        }
    }
}
