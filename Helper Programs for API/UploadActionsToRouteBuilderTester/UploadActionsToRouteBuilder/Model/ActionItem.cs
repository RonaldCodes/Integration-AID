using System;
using Trackmatic.Rest.Planning.Model;

namespace UploadActionsToRouteBuilder
{
    public class ActionItem
    {
        public string ActionReference { get; set; }
        public string CustomerReference { get; set; }
        public string InternalReference { get; set; }
        public string ActionTypeId { get; set; }
        public string ActionTypeName { get; set; }
        public DateTime ExpectedDelivery { get; set; }
        public EActionDirection Direction { get; set; }
        public string Instructions { get; set; }
        public EUnitOfMeasure Measure { get; set; }
        public string ShipToName { get; set; }
        public string ShipToReference { get; set; }
        public string ShipToAddressId { get; set; }
        public string ShipToAddressName { get; set; }
        public TimeSpan MaximumServiceTime { get; set; }
        public string UnitNo { get; set; }
        public string BuildingName { get; set; }
        public string StreetNo { get; set; }
        public string SubDivisionNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string MapCode { get; set; }
        public double Weight { get; set; }
        public int Pieces { get; set; }
        public int Pallets { get; set; }
        public double VolumetricMass { get; set; }
        public double AmountEx { get; set; }
        public double AmountIncl { get; set; }
    }
}
