using System.Collections.Generic;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Dit.Model;

namespace Agent.Model
{
    public class RttDitReturnedAddress
    {
        public string Id { get; set; }
        public string Reference { get; set; }
        public int Cons_Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string ClientId { get; set; }
        public string InvalidReason { get; set; }
        public StructuredAddress Address { get; set; }
        public List<DitContact> Contacts { get; set; }
        public int Status { get; set; }
        public OCoord Coordinates { get; set; }
    }
}
