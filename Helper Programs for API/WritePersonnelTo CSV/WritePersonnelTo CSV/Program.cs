using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Requests;

namespace WritePersonnelTo_CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = Login("9408065009082", "yase191!", "252");

            GetPersonnelInfo(api);
        }

        public static void GetPersonnelInfo(Api api)
        {
            try
            {
                var header = $"FirstName, FullName, Nature, Type, IdentityNumber{Environment.NewLine}";
                var path = $@"C:\Users\YaseenH\Desktop\Adhoc\personnel.csv";
                File.AppendAllText(path, header);

                var batch = new BatchQuery<Personnel>(new BatchOptions
                {
                    Write = 512,
                    Read = 512
                }, api, new LoadPersonnelInBatches(), PersonnelLookup);
                batch.Execute();

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.ToString()}...");
            }
        }

        public static void PersonnelLookup(Api api, Personnel personnel)
        {
            var path = $@"C:\Users\YaseenH\Desktop\Adhoc\personnel.csv";
            var contents = $"{personnel.FirstName}, {personnel.FullName},{personnel.Nature},{personnel.Type},{personnel.IdentityNumber}{Environment.NewLine}";
            File.AppendAllText(path, contents);
        }

        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
