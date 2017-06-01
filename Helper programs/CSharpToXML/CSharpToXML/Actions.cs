using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToXML
{
    public class Actions
    {
        public DateTime CreatedOn { get; set; }
        public string Nature { get; set; }
        public string Reference { get; set; }
        public string InternalReference { get; set; }
        public string ExpectedDeliveryDate { get; set; }
        public string IsCashOnDelivery { get; set; }
        public string Unit { get; set; }
        public string Weight { get; set; }
        public string VolumetricMass { get; set; }
        public string Pieces { get; set; }
        public string Pallets { get; set; }
        public string SpecialInstructions { get; set; }
        public string AmountIncl { get; set; }
        public string AmountExcl { get; set; }
    }
}
