using ExtractDitAddresses.Exceptions;
using System;


namespace ExtractDitAddresses.CSVReader
{
    public class Delivery
    {
        private readonly Line _source;
        private const int ExpectedLength = 19;

        public Delivery(Line source)
        {
            if (source.Data.Length != ExpectedLength)
            {
                throw new FieldFormatException(source.LineNumber, 0, $"Row has an invalid length expected {ExpectedLength} but was {source.Data.Length}");
            }
            _source = source;
        }

        public string GetAction()
        {
            return _source.GetStringByIndex(Constants.Action);
        }

        public string GetAccountNumber()
        {
            return _source.GetStringByIndexEnsureNotNull(Constants.CallRef);
        }

        public string GetAddressId()
        {
            return _source.GetStringByIndexEnsureNotNull(Constants.Look);
        }

        public string GetInvoiceNumber()
        {
            return _source.GetStringByIndexEnsureNotNull(Constants.OrderRef);
        }

        public string GetCustomerName()
        {
            return _source.GetStringByIndexEnsureNotNull(Constants.CallName);
        }

        public double[] GetCoordinates()
        {
            return _source.GetCoordinates(Constants.Address);
        }

        public string GetAddress1()
        {
            return _source.GetStringByIndex(Constants.Address1);
        }

        public string GetAddress2()
        {
            return _source.GetStringByIndex(Constants.Address2);
        }

        public string GetAddress3()
        {
            return _source.GetStringByIndex(Constants.Address3);
        }

        public string GetAddress4()
        {
            return _source.GetStringByIndex(Constants.Address4);
        }

        public int GetQty()
        {
            return _source.GetIntByIndex(Constants.Prod1);
        }

        public double GetAmount()
        {
            return _source.GetDoubleByIndex(Constants.Revenue);
        }

        public string GetOpeningTime()
        {
            return _source.GetStringByIndex(Constants.Open1);
        }

        public string GetClosingTime()
        {
            return _source.GetStringByIndex(Constants.Close1);
        }

        public string GetZone()
        {
            return _source.GetStringByIndex(Constants.Zone);
        }

        public bool GetIsCod()
        {
            return _source.GetIntByIndex(Constants.Cod) > 0;
        }

        public string GetContactPerson()
        {
            return _source.GetStringByIndex(Constants.ContactPerson);
        }

        public string GetTelephone()
        {
            return _source.GetStringByIndex(Constants.Telephone);
        }

        public DateTime GetOrderDate()
        {
            return _source.GetDateTimeByIndex(Constants.OrderDate, "yyyy/MM/dd h:mm");
        }
        
        private static class Constants
        {
            public const int Action = 0;
            public const int CallRef = 1;
            public const int Look = 2;
            public const int OrderDate = 3;
            public const int OrderRef = 4;
            public const int CallName = 5;
            public const int Address = 6;
            public const int Address1 = 7;
            public const int Address2 = 8;
            public const int Address3 = 9;
            public const int Address4 = 10;
            public const int Prod1 = 11;
            public const int Revenue = 12;
            public const int Open1 = 13;
            public const int Close1 = 14;
            public const int Zone = 15;
            public const int Cod = 16;
            public const int ContactPerson = 17;
            public const int Telephone = 18;
        }
    }
}
