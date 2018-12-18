using System;

namespace Routing.ActionSchedule.Model
{
    public class TripActionScheduleReport
    {
        

        public string RouteId { get; set; }

        public string RouteName { get; set; }

        public string Reference { get; set; }

        public string RouteInstructions { get; set; }

        public double MaxSpeed { get; set; }

        public DateTime ScheduledStartDate { get; set; }

        public DateTime ScheduledEndDate { get; set; }

        public double ScheduledDistance { get; set; }

        public string VehicleMake { get; set; }

        public string VehicleModel { get; set; }

        public double VehicleTonnage { get; set; }

        public double VehicleVolume { get; set; }

        public string RegistrationNo { get; set; }

        public string Trailer { get; set; }

        public string CallSign { get; set; }

        public string DriverName1 { get; set; }

        public string CrewName1 { get; set; }

        public string CrewName2 { get; set; }

        public string CrewName3 { get; set; }

        public int StopNo { get; set; }

        public string DecoId { get; set; }

        public string DecoName { get; set; }

        public string DecoAddress { get; set; }

        public double DistanceFromLastStop { get; set; }

        public int EstimatedTravelTime { get; set; } // in minutes

        public string StopLatitude { get; set; }

        public string StopLongitude { get; set; }

        public DateTime? ScheduledETA { get; set; }

        public string DecoInstructions { get; set; }

        public string ActionReference { get; set; }

        public string ActionNature { get; set; }

        public string ActionInternalReference { get; set; }

        public string ActionCustomerReference { get; set; }

        public int ActionPieces { get; set; }

        public string ActionType { get; set; }

        public string ActionTypeId { get; set; }

        public int ActionPallets { get; set; }

        public int RoutePallets { get; set; }

        public double ActionVolume { get; set; }

        public double ActionWeight { get; set; }

        public string ActionSpecialInstructions { get; set; }

        public string CreatedBy { get; set; }

        public double? RouteTemperature { get; set; }

        public string ActionPaymentType { get; set; }

        public double ActionAmountEx { get; set; }

        public double ActionAmountIncl { get; set; }

        public string Bay { get; set; }

        public string StopNoDisplay { get; set; }

        public string EntityName { get; set; }

        public string EntityReference { get; set; }

        public string ActionUnit { get; set; }

        public string OrderNumber { get; set; }

        public string EntityContactName { get; set; }

        public string EntityContactNo { get; set; }

        public string DecoSuburb { get; set; }

        public int DecoMst { get; set; }

        public int EntityMst { get; set; }

        public int IsCod { get; set; }

        public string DeviceImei { get; set; }

        public int IsMobile { get; set; }

        public string InternalReference { get; set; }

        public string ZoneReference { get; set; }

        public string ShipToAddress { get; set; }

    }
}
