using System;

namespace NhibernateExample
{
    public class TripModel
    {
        public virtual int Id { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual double ClientId { get; set; }
        public virtual double NumberofTrips { get; set; }
        public virtual string Color { get; set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string StartAddress { get; set; }
        public virtual string EndAddress { get; set; }
        public virtual double Distance { get; set; }
        public virtual double OptimisedDistance { get; set; }
        public virtual double IdleTime { get; set; }
        public virtual double DrivingTime { get; set; }
        public virtual double NumberOfZoneViolations { get; set; }
        public virtual double NumberOfSpeedViolations { get; set; }
        public virtual double NumberofUnknownStops { get; set; }
        public virtual string CallSign { get; set; }
        public virtual string DateInstalled { get; set; }
        public virtual double ActiveDays { get; set; }
        public virtual string VehicleGroupName { get; set; }
        public virtual string VehicleGroupId { get; set; }
        public virtual string VehicleInternalGroupId { get; set; }
    }
}
