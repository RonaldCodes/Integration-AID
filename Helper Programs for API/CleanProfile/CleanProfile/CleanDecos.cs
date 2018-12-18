using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;

namespace CleanProfile
{
    public class CleanDecos
    {
        public void DeleteDecos(Api api)
        {
            var batch = new BatchQuery<OLocation>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadLocationsInBatches(), DeleteDeco);
            batch.Execute();
        }

        private void DeleteDeco(Api api, OLocation deco)
        {
            if (deco.Id != "301/e0ec0d4f-12c3-4648-a465-36c63399796d")
            {
                var deleteDeco = api.ExecuteRequest(new DeleteLocation(api.Context, deco.Id));
            }
        }
    }
}
