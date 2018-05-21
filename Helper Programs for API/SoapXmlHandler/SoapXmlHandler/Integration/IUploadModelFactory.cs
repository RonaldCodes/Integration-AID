using System.Collections.Generic;
using Agent.Model;
using Trackmatic.Rest.Routing.Model;

namespace Agent.Integration
{
    public interface IUploadModelFactory
    {
        UploadModel Create(RTTTripDetails details, RttSiteDefault site);
    }
}
