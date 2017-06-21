using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToXML
{
    public class HandlingUnit
    {
        public string CustomerReference { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public double VolumetricMass { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public HandlingUnitDimensions Dimensions { get; set; }
        public EUnitOfMeasure UnitOfMeasure { get; set; }
    }

    public enum EUnitOfMeasure
    {
        Box = 0,
        Carton = 1,
        Parcel = 2
    }
}
