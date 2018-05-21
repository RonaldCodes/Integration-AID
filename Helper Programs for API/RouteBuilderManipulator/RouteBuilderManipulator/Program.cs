using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Model;
using Trackmatic.Rest.Planning.Requests;

namespace RouteBuilderManipulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            /* Choose option and update the section */
            //WriteFromOneProfileToTheNext();
            DeletePlannedActions();
            Console.ReadLine();
        }

        public static void DeletePlannedActions()
        {
            var site = new SiteData();
            var controller = new Controller();

            /* Update the below */
            controller.fromDate = new DateTime(2018, 04, 01);
            controller.toDate = new DateTime(2018, 04, 15);
            var forSite = site.LGB();

            /* Does the rest */
            controller.DeletePlannedActionWithLocation(forSite);
        }

        public static void WriteFromOneProfileToTheNext()
        {
            var site = new SiteData();
            var controller = new Controller();

            /* Update the below */
            controller.fromDate = new DateTime(2017, 12, 10);
            controller.toDate = new DateTime(2017, 12, 14);
            var fromSite = site.JLife();
            var toSite = site.TrackmaticTest();

            /* Does the rest */
            var actionsRetrieved = controller.GetPlannedActionsWithLocation(fromSite);
            var actionListToUpload = new List<Trackmatic.Rest.Planning.Model.Action>();
            foreach (var actionRetrieved in actionsRetrieved)
            {
                actionListToUpload.Add(controller.UpdateActionIds(actionRetrieved, fromSite, toSite));
            }
           // controller.UploadToSite(actionListToUpload, toSite);
        }

        public static void WriteToFile(List<Trackmatic.Rest.Planning.Model.Action> planActions)
        {
            var fileName = "UploadedActions.csv";
            var path = @"C:\Users\YaseenH\Desktop\UploadedActions.csv";
            var Header = $"ActionId,Action Ref, EntityId, Entity Ref, DecoId, Deco Ref{Environment.NewLine}";
            File.AppendAllText(path, Header);
            foreach (var action in planActions)
            {
                var contents = $"{action.Id},{action.Reference},{action.Entity.Id},{action.Entity.Reference},{action.Entity.Deco.Id},{action.Entity.Deco.Reference}{Environment.NewLine}";
                File.AppendAllText(path, contents);
            }
        }
    }
}
