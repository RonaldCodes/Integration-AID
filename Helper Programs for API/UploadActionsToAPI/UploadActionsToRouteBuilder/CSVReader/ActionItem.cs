using Agent.Exceptions;
using System;
using System.Globalization;
using Trackmatic.Rest.Planning.Model;

namespace Agent.Csv
{
    public class ActionItem
    {
        private readonly Line _source;
        private const int ExpectedLength = 24;

        public ActionItem(Line source)
        {
            if (source.Data == null)
            {
                throw new FieldFormatException(source.LineNumber, 0, $"No data was present at line {source.LineNumber}");
            }

            if (source.Data.Length != ExpectedLength)
            {
                throw new FieldFormatException(source.LineNumber, 0, $"Row has an invalid length expected {ExpectedLength} but was {source.Data.Length}");
            }
            _source = source;
        }
        public string GetDecoName()
        {
            return _source.GetStringByIndex(Constants.DecoName);
        }
        public string GetCustomerName()
        {
            return _source.GetStringByIndex(Constants.CustomerName);
        }
        public string GetCellNo()
        {
            return _source.GetStringByIndex(Constants.CellNo);
        }

        public string GetEmail()
        {
            return _source.GetStringByIndex(Constants.Email);
        }
        
        public string GetCustomerReference()
        {
            return _source.GetStringByIndex(Constants.CustomerReference);
        }
        public string GetCustomerCode()
        {
            return _source.GetStringByIndex(Constants.CustomerCode);
        }
        public string GetUnitNo()
        {
            return _source.GetStringByIndex(Constants.UnitNo);
        }
        public string GetBuildingName()
        {
            return _source.GetStringByIndex(Constants.BuildingName);
        }
        public string GetStreetNo()
        {
            return _source.GetStringByIndex(Constants.StreetNo);
        }
        public string GetSubDivisionNumber()
        {
            return _source.GetStringByIndex(Constants.SubdivisionNo);
        }
        public string GetStreet()
        {
            return _source.GetStringByIndex(Constants.Street);
        }
        public string GetSuburb()
        {
            return _source.GetStringByIndex(Constants.Suburb);
        }
        public string GetCity()
        {
            return _source.GetStringByIndex(Constants.City);
        }
        public string GetProvince()
        {
            return _source.GetStringByIndex(Constants.Province);
        }
        public string GetPostalCode()
        {
            return _source.GetStringByIndex(Constants.PostalCode);

        }

        public double GetLatitude()
        {
            var latitude = _source.GetStringByIndex(Constants.latitude);
                if (!string.IsNullOrWhiteSpace(latitude))
                {
                    if (latitude != "0")
                    {
                        return double.Parse(latitude, CultureInfo.InvariantCulture);
                    }
                }
                return 0.0;
        }

        public double GetLongitude()
        {
            var longitude = _source.GetStringByIndex(Constants.Longitude);
            if (!string.IsNullOrWhiteSpace(longitude))
            {
                if (longitude != "0")
                {
                    return double.Parse(longitude, CultureInfo.InvariantCulture);
                }
            }
            return 0.0;
        }

        public DateTime GetCreatedOn()
        {
            return _source.GetDateTimeByIndex(Constants.CreatedOn);

        }
        public string GetNature()
        {
            return _source.GetStringByIndex(Constants.Nature);

        }
        public string GetActionReference()
        {
            return _source.GetStringByIndex(Constants.ActionReference);

        }
        public string GetInternalReference()
        {
            return _source.GetStringByIndex(Constants.InternalReference);
        }
        public DateTime GetExpectedDeliveryDate()
        {
            //var test = _source.GetStringByIndex(Constants.ExpectedDeliveryDate);
            return _source.GetDateTimeByIndex(Constants.ExpectedDeliveryDate);

        }
        public int GetPieces()
        {
            return _source.GetIntByIndex(Constants.Pieces);

        }
        public string GetSpecialInstructions()
        {
            return _source.GetStringByIndex(Constants.SpecialInstructions);
        }
         public string GetActionTypeId()
        {
            var deliveryMethod = GetNature();
            return DetermineActionType(deliveryMethod);
        }

        //public EActionDirection DetermineDirection()
        //{
        //    var method = GetActionTypeName().ToLower();
        //    if (method.Contains("delivery") || method.Contains("service"))
        //    {
        //        return EActionDirection.Outbound;
        //    }
        //    return EActionDirection.Inbound;
        //}
        private string DetermineActionType(string method)
        {
            var id = "";

            switch (method)
            {
                case "STO":
                    id = "417/b7627892-6b5b-48da-8e61-521765049094";
                    break;

            }
            return id;
        }

        private static class Constants
        {
            public const int DecoName = 0;
            public const int CustomerName = 1;
            public const int CellNo = 2;
            public const int Email = 3;
            public const int CustomerReference = 4;
            public const int CustomerCode = 5;
            public const int UnitNo = 6;
            public const int BuildingName = 7;
            public const int StreetNo = 8;
            public const int SubdivisionNo = 9;
            public const int Street = 10;
            public const int Suburb = 11;
            public const int City = 12;
            public const int Province = 13;
            public const int PostalCode = 14;
            public const int latitude = 15;
            public const int Longitude = 16;
            public const int CreatedOn = 17;
            public const int Nature = 18;
            public const int ActionReference = 19;
            public const int InternalReference = 20;
            public const int ExpectedDeliveryDate = 21;
            public const int Pieces = 22;
            public const int SpecialInstructions = 23;
        }
    }
}
