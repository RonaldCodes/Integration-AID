using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Requests;

namespace DeleteContacts
{
    public class Program
    {
        static void Main(string[] args)
        {
            var clientId = "";
            var api = CreateLogin("301");

            var contacts = api.ExecuteRequest(new Trackmatic.Rest.Core.Requests.LoadContacts(api.Context)).Data;
        }
        private static Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9408065009082");
            api.Authenticate("yase191!");
            return api;
        }
    }
}
