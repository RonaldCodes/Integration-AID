using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;

namespace CleanProfile
{
    public class CleanEntities
    {
        public void DeleteEntities(Api api)
        {
            var batch = new BatchQuery<Trackmatic.Rest.Routing.Model.Entity>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadEntitiesInBatches(), DeleteEntity);
            batch.Execute();
        }

        private void DeleteEntity(Api api, Trackmatic.Rest.Routing.Model.Entity entity)
        {
            var deleteEntity= api.ExecuteRequest(new DeleteEntity(api.Context, entity.Id));
        }
    }
}
