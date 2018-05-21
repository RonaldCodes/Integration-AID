using Agent.Model;
using System.Linq;

namespace Agent.Integration
{
    public static class Extensions
    {
        public static string GetSizwePlannedEndDate(this RTTTripDetails trip)
        {
            return trip.Consignments.Last().PlannedStopArrivalTime.ToString();
        }

        public static string GetSizwePlannedStartDate(this RTTTripDetails trip)
        {
            return trip.PlannedTripStartAt.ToString();
        }

        public static string GetSizwePlannedTripId(this RTTTripDetails trip)
        {
            return trip.PlannedRouteID;
        }

        public static string GetSizwePlannedOffloadStart(this Consignment consignment)
        {
            return consignment.PlannedStopArrivalTime.ToString();
        }

        public static string GetSizwePlannedOffloadEnd(this Consignment consignment)
        {
            return consignment.PlannedStopDepartureTime.ToString();
        }

        public static string GetSizwePlannedOffloadDuration(this Consignment consignment)
        {
            return consignment.PlannedStopOffloadDuration.ToString();
        }

        public static string GetSizwePlannedDistance(this Consignment consignment)
        {
            return consignment.PlannedTravelDistance.ToString();
        }

        public static string GetSizweAddressId(this Consignment consignment)
        {
            return consignment.Address.SizweAddressId.ToString();
        }

        public static string GetPlannedVehicleClass(this RTTTripDetails trip)
        {
            return trip.Vehicle.VehType.ToString();
        }
    }
}
