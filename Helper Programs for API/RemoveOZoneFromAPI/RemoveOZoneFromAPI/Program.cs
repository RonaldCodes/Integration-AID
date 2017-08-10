using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;

namespace RemoveOZoneFromAPI
{
    class Program
    {

        static void Main(string[] args)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", "110", "9408065009082");
            api.Authenticate("yase191!");

  
            var response = api.ExecuteRequest(new LoadZones(api.Context)).Data;

            foreach (var zone in response)
            {
                var request = new DeleteZone(api.Context, zone.Id);
                api.ExecuteRequest(request);
            }
        }
    }
}
