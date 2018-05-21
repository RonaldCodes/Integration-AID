using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            //    string[] sentences =
            //{
            //    "C# code",
            //    "Chapter 2: Writing Code",
            //    "Unicode",
            //    "no match here"
            //};

            //    string sPattern = "COD";

            //    foreach (string s in sentences)
            //    {
            //        System.Console.Write("{0,24}", s);

            //        if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            //        {
            //            System.Console.WriteLine("  (match for '{0}' found)", sPattern);
            //        }
            //        else
            //        {
            //            System.Console.WriteLine();
            //        }
            //    }

            //    // Keep the console window open in debug mode.
            //    System.Console.WriteLine("Press any key to exit.");
            //    System.Console.ReadKey();


            string email = "COD";
            Regex regex = new Regex(@"COD");
            Match match = regex.Match(email);
            if (match.Success)
                Console.WriteLine(email + " is correct");
            else
                Console.WriteLine(email + " is incorrect");


            string some = "get_last_of_dn";

            var lastOccuranceOf_ = some.LastIndexOf("_");
            Console.WriteLine(some.Substring(0, lastOccuranceOf_));
        }
    }
}
