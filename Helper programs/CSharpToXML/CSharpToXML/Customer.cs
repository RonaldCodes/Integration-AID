using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToXML
{
    public class Customer
    {
        public string DecoName { get; set; }
        public string CustomerName { get; set; }
        public TimeSpan MaximumStopTime { get; set; }
        public string CellNo { get; set; }
        public string Email { get; set; }
        public string CustomerReference { get; set; }
        public string CustomerCode { get; set; }
        public bool IsOnceOffDelivery { get; set; }
        public Address Address { get; set; }
        public List<Actions> Actions { get; set; }
    }
}
