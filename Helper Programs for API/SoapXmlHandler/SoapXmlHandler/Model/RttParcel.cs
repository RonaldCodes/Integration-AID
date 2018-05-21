using System;

namespace Agent.Model
{
    public class Parcel
    {
        public string ParcelID { get; set; }
        public string ConsID { get; set; }
        public string Barcode { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double ActualKG { get; set; }
        public double VolumizerKG { get; set; }
        public double UsedKG { get; set; }
        public string VolCalculated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
