using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestName
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullName = "Jacob";
            var firstName = DetermineFirstName(fullName);
            var lastName = DetermineLastName(fullName);
        }
            
        public static string DetermineFirstName(string ContactName)
        {
            var data = ContactName.Split(' ');
            if (data.Length != 1)
            {
                return data[0];
            }
            return ContactName;
        }

        public static string DetermineLastName(string ContactName)
        {
            var data = ContactName.Split(' ');
            if (data.Length > 1)
            {
                return data[1];
            }
            return null;
        }
    }
}
