using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;

namespace LogUsingHTTP
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            WriteToHttpLogTest1();
            WriteToHttpLogTest2();
        }
        public static void WriteToHttpLogTest1()
        {
            WebRequest request = WebRequest.Create("https://webhook.logentries.com/noformat/logs/ba82fa1c-657a-4168-9022-027b2ee27557");
            request.Method = "POST";
            request.ContentType = "Content-Type: application/json";

            string postData = "Test";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }

        public static void WriteToHttpLogTest2()
        {
            WebRequest request = WebRequest.Create("https://webhook.logentries.com/noformat/logs/ba82fa1c-657a-4168-9022-027b2ee27557");

            var postData = "thing1=hello";
            postData += "&thing2=Logs";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "Content-Type: application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
