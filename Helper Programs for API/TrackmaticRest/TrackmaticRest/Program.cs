using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TrackmaticRest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //   var cookie = AuthenticateAndGetCookie("https://rest.trackmatic.co.za/api/v1/core/security/authenticate", "00000000000404", "tb!AEs8B");
            var cookie = AuthenticateAndGetCookie("https://rest.trackmatic.co.za/api/v1/core/security/authenticate", "9408065009082", "yase191!");
            //   var zone = CallApi("https://rest.trackmatic.co.za/api/v1/planning/zone/get", token, "404", "0", "0");
            GetApiCall(cookie);
        }
        static string AuthenticateAndGetCookie(string url, string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }


        public static void GetApiCall(string cookie)
        {
            var baseAddress = new Uri("https://rest.trackmatic.co.za/api/v1/planning/zone/get");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                cookieContainer.Add(baseAddress, new Cookie(".ASPXFORMSAUTH", cookie));
                var result = client.GetAsync(baseAddress+ "/?c=404&lat=12.12&lng=-22.122").Result;
                result.EnsureSuccessStatusCode();
            }
        }

        //static string CallApi(string url, string token, string clientId, string lat, string lon)
        //{
        //    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        //    using (var client = new HttpClient())
        //    {
        //        if (!string.IsNullOrWhiteSpace(token))
        //        {

        //            client.DefaultRequestHeaders.Clear();
        //            client.DefaultRequestHeaders.Add("Cookie", ".ASPXFORMSAUTH" + token);
        //            //    var pairs = new List<KeyValuePair<string, string>>
        //            //    {
        //            //        new KeyValuePair<string, string> ( "c&", "404" ),
        //            //        new KeyValuePair<string, string> ( "lat&", "0" ),
        //            //        new KeyValuePair<string, string> ( "lng", "0" ),
        //            //    };
        //            //    var content = new FormUrlEncodedContent(pairs);
        //        }
        //        var response = client.GetAsync(url).Result;
        //        return response.Content.ReadAsStringAsync().Result;
        //    }
        //}
    }

}
