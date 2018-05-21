using System;
using System.Collections.Generic;

namespace Agent.Model
{
    public class RTTTripDetails
    {
        public string TripHeaderID { get; set; }
        public int SrcBU { get; set; }
        public int DestBU { get; set; }
        public DateTime? TripDate { get; set; }
        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public DateTime? PlannedTripStartAt { get; set; }
        public List<Consignment> Consignments { get; set; }
        public bool PlannedRoute { get; set; }
        public string PlannedRouteID { get; set; }
    }
}
