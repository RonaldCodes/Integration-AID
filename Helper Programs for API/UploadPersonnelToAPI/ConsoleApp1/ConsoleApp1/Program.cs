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
            var api = new Api("https://rest.trackmatic.co.za/api/v1", "366", "9408065009082");
            api.Authenticate("yase191!");


            string fname = @"Files\PersonnelTemplate.xlsx"; //Read from xls
            Console.WriteLine("Reading file " + fname + ".");

            IWorkbook workBook = WorkbookFactory.Create(new FileStream(Path.GetFullPath(fname), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            for (int i = 0; i < NumberOfSheets(workBook); i++)
            {
                ISheet sheet = workBook.GetSheetAt(i);

                for (int row = 4; row <= sheet.LastRowNum; row++)
                {
                    ////PER Row
                    var Id = "355/" + Guid.NewGuid();//(sheet.GetRow(row).GetCell(3)).ToString();
                    var ClientId = "355";//(sheet.GetRow(row).GetCell(1)).ToString();


                    var firstName = (sheet.GetRow(row).GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();

                    var surname = (sheet.GetRow(row).GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();

                    var idNo = (sheet.GetRow(row).GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                   // var internalReference = (sheet.GetRow(row).GetCell(4, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();

                    var mobilePrimaryNumber = ((sheet.GetRow(row).GetCell(5, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString());
                    var primaryContactNumber = (mobilePrimaryNumber.Trim(new Char[] { ' ', '\'' })).Replace(" ", "");

                    //var secondaryContactNumber =(sheet.GetRow(row).GetCell(7, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var Status = (sheet.GetRow(row).GetCell(9, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
                    //var status = Trackmatic.Rest.Core.Model.EPersonnelStatus.Inactive;
                    //if (Status == "ACTIVE")
                    //{
                    //    status = Trackmatic.Rest.Core.Model.EPersonnelStatus.Active;
                    //}
                    //if (Status == "CASUAL")
                    //{
                    //    status = Trackmatic.Rest.Core.Model.EPersonnelStatus.Inactive;
                    //}

                    var Gender = (sheet.GetRow(row).GetCell(8, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
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

                    //var PersonnelTypes = (sheet.GetRow(row).GetCell(10, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var personnelType = Trackmatic.Rest.Core.Model.EPersonnelType.Driver;
                    //if (PersonnelTypes == "DRIVER")
                    //{
                    //    personnelType = Trackmatic.Rest.Core.Model.EPersonnelType.Driver;
                    //}
                    //if (PersonnelTypes == "CREW")
                    //{
                    //    personnelType = Trackmatic.Rest.Core.Model.EPersonnelType.Crew;
                    //}


                    //var Nature = (sheet.GetRow(row).GetCell(8, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString().ToUpper();
                    //var nature = Trackmatic.Rest.Core.Model.EPersonnelNature.Casual;
                    //if (Nature == "EMPLOYEE")
                    //{
                    //    nature = Trackmatic.Rest.Core.Model.EPersonnelNature.Employee;
                    //}
                    //if (Nature == "OWNER DRIVER")
                    //{
                    //    nature = Trackmatic.Rest.Core.Model.EPersonnelNature.OwnerDriver;
                    //}
                    //if (Nature == "THIRD PARTY")
                    //{
                    //    nature = Trackmatic.Rest.Core.Model.EPersonnelNature.ThirdParty;
                    //}
                    //if (Nature == "CASUAL")
                    //{
                    //    nature = Trackmatic.Rest.Core.Model.EPersonnelNature.Casual;
                    //}

                    //var Nationality = (sheet.GetRow(row).GetCell(13, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();




                    //var Default = (sheet.GetRow(row).GetCell(12, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();

                    //var Tag = (sheet.GetRow(row).GetCell(14, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var CreatedOn = (sheet.GetRow(row).GetCell(15, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
                    //var createdOn = new DateTime();
                    //if (!string.IsNullOrWhiteSpace(CreatedOn))
                    //{
                    //    createdOn = DateTime.Parse(CreatedOn);
                    //}
                    //var NickName = (sheet.GetRow(row).GetCell(16, MissingCellPolicy.CREATE_NULL_AS_BLANK)).ToString();
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

                        FirstName = firstName,
                        LastName = surname,
                        IdentityNumber = idNo,
                        //ReferenceNo = internalReference,
                        PrimaryContactNo = primaryContactNumber,
                        //SecondaryContactNo = Number,
                        //Status = status,
                        Gender = gender,
                        //Type = type,
                        //Nature = nature,
                        //Nationality = Nationality,
 
                        //DefaultVehicleRegistrationNumber = Default,
                        
                        //Tag = Tag,
                        //CreatedOn = createdOn,
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
