using FluentNHibernate.Mapping;

namespace NhibernateExample
{
    public class TripMapper: ClassMap<TripModel>
    {
        public TripMapper()
        {
            Table("trip");
            Id(m => m.Id);
            Map(m => m.RegistrationNumber);
            Map(m => m.ClientId);
            Map(m => m.NumberofTrips);
            Map(m => m.Color);
            Map(m => m.Make);
            Map(m => m.Model);
            Map(m => m.StartDate);
            Map(m => m.EndDate);
            Map(m => m.StartAddress);
            Map(m => m.EndAddress);
            Map(m => m.Distance);
            Map(m => m.OptimisedDistance);
            Map(m => m.IdleTime);
            Map(m => m.DrivingTime);
            Map(m => m.NumberOfZoneViolations);
            Map(m => m.NumberOfSpeedViolations);
            Map(m => m.NumberofUnknownStops);
            Map(m => m.CallSign);
            Map(m => m.DateInstalled);
            Map(m => m.ActiveDays);
            Map(m => m.VehicleGroupName);
            Map(m => m.VehicleGroupId);
            Map(m => m.VehicleInternalGroupId);
        }

    }
}
