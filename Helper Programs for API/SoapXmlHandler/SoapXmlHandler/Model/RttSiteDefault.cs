using Trackmatic.Rest.Core.Model;

namespace Agent.Model
{
    public class RttSiteDefault
    {
        public string Id { get; set; }
        public string RttTemplateId { get; set; }
        public string RttActionTypeDelivery { get; set; }
        public string RttActionTypeCollection { get; set; }
        public OLocation StaticDeco { get; set; }
    }
}
