using System;
using System.Collections.Generic;
using System.Linq;
using Trackmatic.Rest.Batch.Requests;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;
using static FruitSpotAllEntities.Utility;

namespace FruitSpotAllEntities
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "Entity_Location_Info_NGF2.csv";
            var api = Login("9408065009082", "yase191!", "144");
            var take = 500;
            var NumberOfPages = GetNumberOfPages(api,take);

            for (int i = 0; i<= NumberOfPages; i++)
            {
                var percentage = Math.Round(i*100/NumberOfPages);
                var skip = take * i;

                Console.WriteLine($"{percentage}% Completed...");
                WriteToFile(api, fileName, take, skip);
            }
            Console.ReadLine();
        }

        private static void WriteToFile(Api api, string fileName, int take , int skip)
        {
            var entities = api.ExecuteRequest(new LoadEntities(api.Context, null, take, skip)).Data.Data; ;

            foreach (var entity in entities)
            {
                var decos = entity.Decos;
                var info = new List<FileLine> { };
                if (decos.Count != 0)
                {
                    try
                    {
                        var deco = api.ExecuteRequest(new LoadLocation(api.Context, decos.First().DecoId)).Data;

                        info = new List<FileLine>
                        {
                            //Wrap(entity.Id,"Entity Id"),
                             Wrap(entity.Reference,"Entity Reference"),
                              Wrap(entity.Name,"Entity Name"),
                              Wrap(entity.Address, "Entity Address"),
                              // Wrap(deco.DecoId,"Deco Id"),
                              Wrap(deco.Reference,"Location Reference"),
                              Wrap(deco.Name,"Location Name"),
                              Wrap(deco.Shape,"Location Shape"),
                                 Wrap(deco.StructuredAddress.SubDivisionNumber,"Location SubDivisionNumber"),
                                 Wrap(deco.StructuredAddress.UnitNo,"Location UnitNo"),
                                 Wrap(deco.StructuredAddress.BuildingName,"Location Building Name"),
                                 Wrap(deco.StructuredAddress.StreetNo,"Location StreetNo"),
                                 Wrap(deco.StructuredAddress.Street,"Location Street"),
                                 Wrap(deco.StructuredAddress.Suburb,"Location Suburb"),
                                 Wrap(deco.StructuredAddress.PostalCode,"Location Postal Code"),
                                 Wrap(deco.StructuredAddress.City,"Location City"),
                                 Wrap(deco.StructuredAddress.Province,"Location Province"),
                        };
                    }
                    catch (Exception e)
                    {
                        info = new List<FileLine>
                        {
                            //Wrap(entity.Id,"Entity Id"),
                             Wrap(entity.Reference,"Entity Reference"),
                              Wrap(entity.Name,"Entity Name"),
                              Wrap(entity.Address, "Entity Address"),
                              // Wrap(deco.DecoId,"Deco Id"),
                              Wrap("","Location Reference"),
                              Wrap("","Location Name"),
                              Wrap("","Location Shape"),
                                 Wrap("","Location SubDivisionNumber"),
                                 Wrap("","Location UnitNo"),
                                 Wrap("","Location Building Name"),
                                 Wrap("","Location StreetNo"),
                                 Wrap("","Location Street"),
                                 Wrap("","Location Suburb"),
                                 Wrap("","Location Postal Code"),
                                 Wrap("","Location City"),
                                 Wrap("","Location Province"),
                        };
                    }

                }
                else
                {
                    info = new List<FileLine>
                        {
                            //Wrap(entity.Id,"Entity Id"),
                             Wrap(entity.Reference,"Entity Reference"),
                              Wrap(entity.Name,"Entity Name"),
                              Wrap(entity.Address, "Entity Address"),
                              // Wrap(deco.DecoId,"Deco Id"),
                              Wrap("","Location Reference"),
                              Wrap("","Location Name"),
                              Wrap("","Location Shape"),
                                 Wrap("","Location SubDivisionNumber"),
                                 Wrap("","Location UnitNo"),
                                 Wrap("","Location Building Name"),
                                 Wrap("","Location StreetNo"),
                                 Wrap("","Location Street"),
                                 Wrap("","Location Suburb"),
                                 Wrap("","Location Postal Code"),
                                 Wrap("","Location City"),
                                 Wrap("","Location Province"),
                        };
                }
                Append(fileName, info);

                //foreach (var deco in decos)
                //{

                //}
            }
        }
        private static double GetNumberOfPages(Api api, int take)
        {
          return Math.Ceiling((api.ExecuteRequest(new LoadEntities(api.Context, null, 1, 0)).Data.Total) / take);
        }
        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
