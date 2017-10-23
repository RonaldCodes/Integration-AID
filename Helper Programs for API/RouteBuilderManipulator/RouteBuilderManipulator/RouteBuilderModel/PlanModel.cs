using System;
using System.Linq;
using Trackmatic.Rest.Core;

namespace RouteBuilderManipulator
{
    public class Plan
    {
        public void DeletePlan(Api api, int take, int skip, DateTime startDate, DateTime endDate)
        {
            var plan = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.SearchPlans(api.Context, null, take, skip)).Data;
            var planIds = plan.Data.Select(p => p.Id);

            foreach (var planId in planIds)
            {
                var planDelete = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.DeletePlan(api.Context, planId));
            }
        }
    }
}
