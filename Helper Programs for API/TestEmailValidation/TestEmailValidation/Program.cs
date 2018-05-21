using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestEmailValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            var testEmail = "";
            var result = IsValidEmail(testEmail);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

            //try
            //{
            //    MailAddress m = new MailAddress(email);
            //    return true;
            //}
            //catch(FormatException)
            //{
            //    return false;
            //}
        }
    }
}
