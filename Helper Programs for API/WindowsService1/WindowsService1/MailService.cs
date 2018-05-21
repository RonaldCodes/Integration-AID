using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

namespace WindowsService1
{
    public partial class MailService : ServiceBase
    {
        public void Scheduler()
        {
            InitializeComponent();
            var strTime = Convert.ToInt32(ConfigurationManager.AppSettings["callDuration"]);
            int getCallType = Convert.ToInt32(ConfigurationManager.AppSettings["CallType"]);
            if (getCallType == 1)
            {
                var timer1 = new System.Timers.Timer();
                double inter = (double)GetNextInterval();
                timer1.Interval = inter;
                //timer1.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick(,getCallType,timer1));
            }
            else
            {
                var timer1 = new System.Timers.Timer();
                timer1.Interval = strTime * 1000;
                //timer1.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick(,getCallType, timer1));
            }
        }
        public MailService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //timer1.AutoReset = true;
            //timer1.Enabled = true;
            ServiceLog.WriteErrorLog("Daily Reporting service started");
        }

        protected override void OnStop()
        {
            //timer1.AutoReset = false;
            //timer1.Enabled = false;
            ServiceLog.WriteErrorLog("Daily Reporting service stopped");
        }
        private double GetNextInterval()
        {
            var timeString = ConfigurationManager.AppSettings["StartTime"];
            DateTime t = DateTime.Parse(timeString);
            TimeSpan ts = new TimeSpan();
            int x;
            ts = t - System.DateTime.Now;
            if (ts.TotalMilliseconds < 0)
            {
                ts = t.AddDays(1) - System.DateTime.Now;//Here you can increase the timer interval based on your requirments.   
            }
            return ts.TotalMilliseconds;
        }
        private void SetTimer(Timer timer1)
        {
            try
            {
                double inter = (double)GetNextInterval();
                timer1.Interval = inter;
                timer1.Start();
            }
            catch (Exception ex)
            {
            }
        }
        private void ServiceTimer_Tick(object sender, int getCallType, Timer timer1)
        {
            string Msg = "Hi ! This is DailyMailSchedulerService mail.";//whatever msg u want to send write here.  
                                                                        // Here you can write the   
            ServiceLog.SendEmail("manishki007@gmail.com", "abc@live.com", "manishki@live.com", "Daily Report of DailyMailSchedulerService on " + DateTime.Now.ToString("dd-MMM-yyyy"), Msg);

            if (getCallType == 1)
            {
                timer1.Stop();
                System.Threading.Thread.Sleep(1000000);
                SetTimer(timer1);
            }
        }
    }
}
