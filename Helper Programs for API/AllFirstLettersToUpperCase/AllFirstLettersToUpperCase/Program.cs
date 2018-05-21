using System;
using System.Globalization;

namespace AllFirstLettersToUpperCase
{
    class Program
    {
        static void Main(string[] args)
        {
            //string s = "THIS IS MY TEXT RIGHT NOW";
            string s = "ANOTHER TEST";

            s = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());

            //OR
            //s = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());

            Console.WriteLine(s);
        }
    }
}
