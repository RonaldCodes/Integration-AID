using System;
using Agent.Exceptions;

namespace Agent.Csv
{
    public class ActionLine
    {
        private readonly Line _source;
        private const int expectedLength = 26;

        public ActionLine(Line source)
        {
            if (source.Data != null)
            {
                if (source.Data.Length != expectedLength)
                {
                    throw new FieldFormatException(source.LineNumber, 0, $"Row has an invalid length expected {expectedLength} but was {source.Data.Length}");
                }
                _source = source;
            }
        }

        public int GetDeliveryNo()
        {
            return _source.GetIntByIndex(Constants.DeliveryNo);
        }

        public string GetDeliveryItem()
        {
            return _source.GetStringByIndex(Constants.DeliveryItem);
        }

        public DateTime GetDeliveryDate()
        {
            return _source.GetDateTimeByIndex(Constants.DeliveryDate,"yyyyMMdd");
        }

        public string GetRoute()
        {
            return _source.GetStringByIndex(Constants.Route);
        }

        public int GetGroupNo()
        {
            return _source.GetIntByIndex(Constants.GroupNo);
        }

        public string GetMaterialNo()
        {
            return _source.GetStringByIndex(Constants.MaterialNo);
        }

        public string GetMaterialDescription()
        {
            return _source.GetStringByIndex(Constants.MaterialDescription);
        }

        public int GetQty()
        {
            return (int)Math.Round(_source.GetDoubleByIndex(Constants.Qty));
        }

        public string GetUoM()
        {
            return _source.GetStringByIndex(Constants.UoM);
        }

        public double GetWeight()
        {
            return _source.GetDoubleByIndex(Constants.Weight);
        }

        public double GetVolume()
        {
            return _source.GetDoubleByIndex(Constants.Volume);
        }

        public string GetCustomerNo()
        {
            return _source.GetStringByIndex(Constants.CustomerNo);
        }

        public string GetCustomerName()
        {
            return _source.GetStringByIndex(Constants.CustomerName);
        }

        public string GetCustomerReference()
        {
            return _source.GetStringByIndex(Constants.CustomerReference);
        }

        public string GetStreetNumber()
        {
            return _source.GetStringByIndex(Constants.StreetNumber);
        }

        public string GetStreetName()
        {
            return _source.GetStringByIndex(Constants.StreetName);
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

        public string GetTelephoneNo()
        {
            return _source.GetStringByIndex(Constants.TelephoneNo);
        }

        public string GetFaxNo()
        {
            return _source.GetStringByIndex(Constants.FaxNo);
        }

        public string GetStoreName()
        {
            return _source.GetStringByIndex(Constants.StoreName);
        }

        public string GetCentrumName()
        {
            return _source.GetStringByIndex(Constants.CentrumName);
        }

        public string GetTransportationZone()
        {
            return _source.GetStringByIndex(Constants.TransportationZone);
        }
        public string GetRegistration()
        {
            return _source.GetStringByIndex(Constants.Registration);
        }
        private static class Constants
        {
            public const int DeliveryNo = 0;
            public const int DeliveryItem = 1;
            public const int DeliveryDate = 2;
            public const int Route = 3;
            public const int GroupNo = 4;
            public const int MaterialNo = 5;
            public const int MaterialDescription = 6;
            public const int Qty = 7;
            public const int UoM = 8;
            public const int Weight = 9;
            public const int Volume = 10;
            public const int CustomerNo = 11;
            public const int CustomerName = 12;
            public const int CustomerReference = 13;
            public const int StreetNumber = 14;
            public const int StreetName = 15;
            public const int Suburb = 16;
            public const int City = 17;
            public const int Province = 18;
            public const int PostalCode = 19;
            public const int TelephoneNo = 20;
            public const int FaxNo = 21;
            public const int StoreName = 22;
            public const int CentrumName = 23;
            public const int TransportationZone = 24;
            public const int Registration = 25;
        }
    }
}
