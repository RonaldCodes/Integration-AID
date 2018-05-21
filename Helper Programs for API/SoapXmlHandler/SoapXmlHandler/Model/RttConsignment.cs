using System;
using System.Collections.Generic;

namespace Agent.Model
{
    public class Consignment
    {
        public string TripPODNo { get; set; }
        public string ConsType { get; set; }
        public List<Reference> References { get; set; }
        public string ConsignmentNo { get; set; }
        public int ConsignmentID { get; set; }
        public Account Account { get; set; }
        public int TotalParcelCount { get; set; }
        public Address Address { get; set; }
        public List<Parcel> Parcels { get; set; }
        public Rica RICA { get; set; }
        public long StopNumber { get; set; }
        public DateTime? PlannedStopArrivalTime { get; set; }
        public DateTime? PlannedStopDepartureTime { get; set; }
        public int PlannedStopOffloadDuration { get; set; }
        public int PlannedTravelDuration { get; set; }
        public double PlannedTravelDistance { get; set; }

        public bool IsCollection()
        {
            if (ConsType.ToLower() == "collection") return true;
            return false;
        }
    }
}
