using System;
using System.Collections.Generic;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Requests;
using static ExtractPersonnelfromApi.Utility;

namespace ExtractPersonnelfromApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "People206.csv"; //Write to a csv 
            var api = Login("9408065009082", "yase191!", "366");
            var take = 500;
            var NumberOfPages = GetNumberOfPages(api, take);

            for (int i = 0; i <= NumberOfPages; i++)
            {
                var percentage = Math.Round(i * 100 / NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                WriteToFile(api, fileName, take, skip);
            }
            Console.ReadLine();
        }

        private static void WriteToFile(Api api, string fileName, int take, int skip)
        {
            var people = api.ExecuteRequest(new LoadPersonnels(api.Context, null, take, skip)).Data.Data;
            foreach (var person in people)
            {
                //var persondId = "228/" + person.Id.Substring(person.Id.LastIndexOf('/') + 1);
                //var info = new List<FileLine>
                //        {

                //               Wrap(persondId,"Id"),
                //               Wrap("228","ClientId"),
                //               Wrap(person.FirstName, "First Name"),
                //               Wrap(person.LastName, "Surname"),
                //               Wrap(person.Gender, "Gender"),
                //               Wrap(person.IdentityNumber,"Id No"),
                //               Wrap(person.PrimaryContactNo,"Mobile"),
                //               Wrap(person.SecondaryContactNo,"Number"),
                //               Wrap(person.Nature,"Nature"),
                //               Wrap(person.Status,"Status"),
                //               Wrap(person.Type,"Type"),
                //               Wrap(person.ReferenceNo,"Internal Ref"),
                //               Wrap(person.DefaultVehicleRegistrationNumber,"Default RegNo"),
                //               Wrap(person.Nationality, "Nationality"),
                //               Wrap(person.Tag, "Tag"),
                //               Wrap(person.CreatedOn, "CreatedOn"),
                //               Wrap(person.NickName, "NickName"),
                //               Wrap(person.LicenseExpiryDate, "LicenseExpiryDate"),
                //               Wrap(person.LicenseId, "LicenseId"),
                //               Wrap(person.LicenseType, "LicenseType"),

                //        };
                //Append(fileName, info);

                //Api call to delete from API:
                var DeletePerson = api.ExecuteRequest(new DeletePersonnel(api.Context, person.Id));

            }
        }

        private static double GetNumberOfPages(Api api, int take)
        {
            return Math.Ceiling((api.ExecuteRequest(new LoadPersonnels(api.Context, null, 1, 0)).Data.Total) / take);
        }
        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
