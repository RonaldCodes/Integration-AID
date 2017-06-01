using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToXML
{
    public class Route
    {
        public string RouteName { get; set; }
        public string Sequence { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StartDate { get; set; }
        public string ReferenceNo { get; set; }
        public string VehicleRegistration { get; set; }
        public string DriverName { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
