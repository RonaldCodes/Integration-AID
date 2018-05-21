using Agent.Model;
using Trackmatic.Rest.Dit.Model;

namespace Agent.Integration
{
    public interface IUploadAddressFactory
    {
        DitAddress Create(RttDitAddress address);
    }
}
