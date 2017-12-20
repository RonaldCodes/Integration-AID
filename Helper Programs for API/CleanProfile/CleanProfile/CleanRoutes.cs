using System;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Requests;
using Trackmatic.Rest.Routing.Model;
using System.Collections.Generic;

namespace CleanProfile
{
    public class CleanRoutes
    {
        public void DeleteRoutes(Api api)
        {
            var routes = api.ExecuteRequest(new SearchRouteInstances(api.Context, new RouteInstanceSearchCriteria
            {
                Take = 1024
            })).Data;

            foreach (var route in routes.Data)
            {
                if (route.Reference != "")
                {
                    continue;
                }
                api.ExecuteRequest(new DeleteRouteRequest(api.Context, route.Id));
                Console.WriteLine(route.Id);
            }
        }

        public void DeleteRoutesById(Api api, List<string> routesToDelete)
        { 
            foreach (var routeId in routesToDelete)
            {
                api.ExecuteRequest(new DeleteRouteRequest(api.Context, routeId));
                Console.WriteLine(routeId);
            }
        }

        //public void DeleteRoutes(Api api)
        //{
        //    var batch = new BatchQuery<Route>(new BatchOptions
        //    {
        //        Write = 512,
        //        Read = 512
        //    }, api, new LoadRoutesInBatches(), DeleteRoute);
        //    batch.Execute();
        //}

        //private void DeleteRoute(Api api, Route route)
        //{
        //    var deleteRoute = api.ExecuteRequest(new DeleteRouteRequest(api.Context, route.Id));
        //}
    }
}
