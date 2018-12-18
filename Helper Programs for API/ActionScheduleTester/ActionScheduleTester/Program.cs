using System;
using System.Collections.Generic;
using ActionScheduleTranspharm = Routing.ActionSchedule.Transpharm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Framework.Security;
using Trackmatic.Reports.Interfaces;
using Tests.Routing.Common.Artifacts;
using System.IO;
using Tests.Routing.Common.Renderers;

namespace ActionScheduleTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            Tranpharm_report_runs_and_renders();
        }
        public static void Tranpharm_report_runs_and_renders()
        {
            RenderTranspharm("566/1214754");
        }
        private static void RenderTranspharm(string id)
        {

            var generator = new ActionScheduleTranspharm.Generator();

            var report = new Report();

            var identity = new DummyIdentity();

            var client = id.Split('/')[0];

            var arguments = new List<ReportArgument>
                {
                    new ReportArgument
                        {
                            Name = "id",
                            Value = id
                        }
                };


            var context = new DefaultClientContext(string.Empty);

            var result = generator.Generate(report, context, identity, arguments);


            var rendered = RenderAsPdf("../../../Routing.ActionSchedule.Transpharm/" + client + ".rpt", result);

            File.WriteAllBytes("schedule." + client + ".pdf", rendered);
        }
        public static byte[] RenderAsPdf(string path, CompiledReport report)
        {
            var renderer = new CrystalRenderer();
            return renderer.Render(path, report);
        }
    }
}
