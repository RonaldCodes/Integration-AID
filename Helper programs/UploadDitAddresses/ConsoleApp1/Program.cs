using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Dit.Model;

namespace UploadDitAddress
{
    class Program
    {
       
        static void Main(string[] args)
        {
            List<DitAddress> list = new List<DitAddress>();
            var clientId = "371";
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9408065009082");
            api.Authenticate("yase191!");


            string fname = @"Waltons.xlsx"; //Read from xls
            Console.WriteLine("Reading file " + fname + ".");

            IWorkbook workBook = WorkbookFactory.Create(new FileStream(Path.GetFullPath(fname), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            for (int i = 0; i < NumberOfSheets(workBook); i++)
            {
                ISheet sheet = workBook.GetSheetAt(i);

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    ////PER Row
                    var cell0 = (sheet.GetRow(row).GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell1 = (sheet.GetRow(row).GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell2 = (sheet.GetRow(row).GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell3 = (sheet.GetRow(row).GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var cell4 = (sheet.GetRow(row).GetCell(4, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var cell5 = (sheet.GetRow(row).GetCell(5, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var cell6 = (sheet.GetRow(row).GetCell(6, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell7 = (sheet.GetRow(row).GetCell(7, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell8 = (sheet.GetRow(row).GetCell(8, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell9 = (sheet.GetRow(row).GetCell(9, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell10 = (sheet.GetRow(row).GetCell(10, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell11 = (sheet.GetRow(row).GetCell(11, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell12 = (sheet.GetRow(row).GetCell(12, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var cell13 = (sheet.GetRow(row).GetCell(13, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();

                    var address = new DitAddress
                    {
                        Id = $"{clientId}/{cell2}",
                        ClientId = clientId,
                        Address = new StructuredAddress
                        {
                            Street = RemoveZaf(cell10),
                            Suburb = RemoveZaf(cell11),                            
                            City = RemoveZaf(cell12),
                            PostalCode = RemoveZaf(cell13),
                        },
                        Name = cell3,
                        CorrelationId = cell1,
                        Reference = $"{cell7}-{cell0}",
                        ZoneReference = cell7,
                        Coordinates = new OCoord
                        {
                            Longitude = Convert.ToDouble(cell9),
                            Latitude = Convert.ToDouble(cell8),
                        },

                    };
                    list.Add(address);
                }
            }
            Console.WriteLine(Upload(api,list));
        }
        public static string RemoveZaf(string address)
        {
            if (address.ToLower().Contains("zaf"))
            {
                var newAddress = address.Replace("ZAF", "");
                return newAddress;
            }
            return address;
        }

        public static string Upload(Api api, List<DitAddress> addresses)
        {
            //api.ExecuteRequest(new UploadAddresses(api.Context, addresses));
            return "Done";
        }
        public static int NumberOfSheets(IWorkbook workBook)
        {
            return workBook.NumberOfSheets;
        }
    }
}
