using System;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Requests;

namespace CleanProfile
{
    public class CleanActions
    {
        public void DeleteActions(Api api, DateTime fromDate, DateTime toDate)
        {
            var batch = new BatchQuery<Trackmatic.Rest.Routing.Model.Action>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadActionsInBatches(fromDate, toDate), DeleteAction);
            batch.Execute();
        }

        private void DeleteAction(Api api, Trackmatic.Rest.Routing.Model.Action action)
        {
            var deleteAction = api.ExecuteRequest(new DeleteAction(api.Context, action.Id));
        }
    }
}
