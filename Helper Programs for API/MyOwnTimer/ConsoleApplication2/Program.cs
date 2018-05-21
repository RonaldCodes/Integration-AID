using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication2
{

    public class Program
    {
       static DateTime currentDate = DateTime.Today.AddDays(-1);
        public static DateTime Date { get; private set; }
        public static void Main()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;

            Console.WriteLine("Press \'q\' to quit the sample.");

            while (Console.Read() != 'q') ;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello World!");
            if (DateTime.Today != Date)
            {
                doSomething();
            }
        }
        //static void Main(string[] args)
        //{

        //    if (DateTime.Today != currentDate)
        //    {
        //        
        //    }
        //}
        public static void doSomething()
        {
            Console.WriteLine("Hello");
                Dictionary<string, string> profiles = new Dictionary<string, string>()
            {
                    {"Bridgestone", "435"}
            };

            string value; 
            profiles.TryGetValue("Bridgestone", out value);
            currentDate = DateTime.Today;
            Date = DateTime.Today;
        }
    }
}
