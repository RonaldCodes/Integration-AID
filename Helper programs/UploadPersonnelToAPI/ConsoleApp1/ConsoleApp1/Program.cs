using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Core.Requests;
using static UploadPersonnelToApi.Utility;

namespace UploadPersonnelToApi
{
    class Program
    {
       
        static void Main(string[] args)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", "228", "9408065009082");
            api.Authenticate("yase191!");


            string fname = @"People206.xls"; //Read from xls
            Console.WriteLine("Reading file " + fname + ".");

            IWorkbook workBook = WorkbookFactory.Create(new FileStream(Path.GetFullPath(fname), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            for (int i = 0; i < NumberOfSheets(workBook); i++)
            {
                ISheet sheet = workBook.GetSheetAt(i);

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    ////PER Row
                    var Id = (sheet.GetRow(row).GetCell(0)).ToString();
                    var ClientId = (sheet.GetRow(row).GetCell(1)).ToString();
                    var FirstName = (sheet.GetRow(row).GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Surname = (sheet.GetRow(row).GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Gender =(sheet.GetRow(row).GetCell(4, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
                    var gender = Trackmatic.Rest.Core.Model.EGender.Unknown;
                    if (Gender == "MALE")
                    {
                        gender = Trackmatic.Rest.Core.Model.EGender.Male;
                    }
                    if (Gender == "FEMALE")
                    {
                        gender = Trackmatic.Rest.Core.Model.EGender.Female;
                    }
                    if (Gender == "Unknown")
                    {
                        gender = Trackmatic.Rest.Core.Model.EGender.Unknown;
                    }
                    var IdNo = (sheet.GetRow(row).GetCell(5, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Mobile = (sheet.GetRow(row).GetCell(6, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Number = (sheet.GetRow(row).GetCell(7, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Nature = (sheet.GetRow(row).GetCell(8, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
                    var nature = Trackmatic.Rest.Core.Model.EPersonnelNature.Casual;
                    if (Nature == "EMPLOYEE")
                    {
                        nature = Trackmatic.Rest.Core.Model.EPersonnelNature.Employee;
                    }
                    if (Nature == "OWNER DRIVER")
                    {
                        nature = Trackmatic.Rest.Core.Model.EPersonnelNature.OwnerDriver;
                    }
                    if (Nature == "THIRD PARTY")
                    {
                        nature = Trackmatic.Rest.Core.Model.EPersonnelNature.ThirdParty;
                    }
                    if (Nature == "CASUAL")
                    {
                        nature = Trackmatic.Rest.Core.Model.EPersonnelNature.Casual;
                    }

                    var Status = (sheet.GetRow(row).GetCell(9, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
                    var status = Trackmatic.Rest.Core.Model.EPersonnelStatus.Inactive;
                    if (Status == "ACTIVE")
                    {
                        status = Trackmatic.Rest.Core.Model.EPersonnelStatus.Active;
                    }
                    if (Status == "CASUAL")
                    {
                        status = Trackmatic.Rest.Core.Model.EPersonnelStatus.Inactive;
                    }

                    var Type = (sheet.GetRow(row).GetCell(10, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var type = Trackmatic.Rest.Core.Model.EPersonnelType.Driver;
                    if (Type == "DRIVER")
                    {
                        type = Trackmatic.Rest.Core.Model.EPersonnelType.Driver;
                    }
                    if (Type == "CREW")
                    {
                        type = Trackmatic.Rest.Core.Model.EPersonnelType.Crew;
                    }
                    var InternalRef = (sheet.GetRow(row).GetCell(11, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Default = (sheet.GetRow(row).GetCell(12, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Nationality = (sheet.GetRow(row).GetCell(13, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var Tag = (sheet.GetRow(row).GetCell(14, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var CreatedOn = (sheet.GetRow(row).GetCell(15, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var createdOn = new DateTime();
                    if (!string.IsNullOrWhiteSpace(CreatedOn))
                    {
                        createdOn = DateTime.Parse(CreatedOn);
                    }
                    var NickName = (sheet.GetRow(row).GetCell(16, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var LicenseExpiryDate = (sheet.GetRow(row).GetCell(17, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var licenseExpiryDate = new DateTime();
                    //if (!string.IsNullOrWhiteSpace(LicenseExpiryDate))
                    //{
                    //    licenseExpiryDate = DateTime.Parse(LicenseExpiryDate);
                    //}
                    //var LicenseId = (sheet.GetRow(row).GetCell(18, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
       
                    //var LicenseType = (sheet.GetRow(row).GetCell(19, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
                    //var licenseType = Trackmatic.Rest.Core.Model.ELicenseType.Unknown;
                    //if (LicenseType == "Unknown")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.Unknown;
                    //}
                    //if (LicenseType == "CodeA")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.CodeA;
                    //}
                    //if (LicenseType == "CodeB")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.CodeB;
                    //}
                    //if (LicenseType == "CodeC")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.CodeC;
                    //}
                    //if (LicenseType == "CodeD")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.CodeD;
                    //}
                    //if (LicenseType == "A")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.A;
                    //}
                    //if (LicenseType == "A1")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.A1;
                    //}
                    //if (LicenseType == "B")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.B;
                    //}
                    //if (LicenseType == "C")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.C;
                    //}
                    //if (LicenseType == "C1")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.C1;
                    //}
                    //if (LicenseType == "EB")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.EB;
                    //}
                    //if (LicenseType == "EC")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.EC;
                    //}
                    //if (LicenseType == "EC1")
                    //{
                    //    licenseType = Trackmatic.Rest.Core.Model.ELicenseType.EC1;
                    //}

                    //For blanks
                    //var someData = (sheet.GetRow(row).GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    var p = new Personnel
                    {
                  

                        Id = Id,
                        ClientId = ClientId,
                        FirstName = FirstName,
                        LastName = Surname,
                        Gender = gender,
                        IdentityNumber = IdNo,
                        PrimaryContactNo = Mobile,
                        SecondaryContactNo = Number,
                        Nature = nature,
                        Status = status,
                        Type = type,
                        ReferenceNo = InternalRef,
                        DefaultVehicleRegistrationNumber = Default,
                        Nationality = Nationality,
                        //Tag = Tag,
                        CreatedOn = createdOn,
                        //NickName = NickName,
                        //LicenseExpiryDate = licenseExpiryDate,
                        //LicenseId = LicenseId,
                        //LicenseType = licenseType,

                    };

             
                    api.ExecuteRequest(new SavePersonnel(api.Context, p));
                }
            }


        }

        public static int NumberOfSheets(IWorkbook workBook)
        {
            return workBook.NumberOfSheets;
        }
    }
}
