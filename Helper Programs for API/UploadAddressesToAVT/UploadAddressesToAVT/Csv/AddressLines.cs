using UploadAddressesToAVT.Exceptions;

namespace UploadAddressesToAVT.Csv
{
    public class AddressLines
    {
        private readonly Line _source;
        private const int ExpectedLength = 8;

        public AddressLines(Line source)
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

        public string GetReference()
        {
            return _source.GetStringByIndex(Constants.Reference);
        }

        public string GetName()
        {
            return _source.GetStringByIndex(Constants.Reference);
        }

        public double GetLatitude()
        {
            return _source.GetDoubleByIndex(Constants.Latitude);
        }

        public double GetLongitude()
        {
            return _source.GetDoubleByIndex(Constants.Longitude);
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

        private static class Constants
        {
            public const int Reference = 0;
            public const int Name = 1;
            public const int Latitude = 2;
            public const int Longitude = 3;
            public const int UnitNo = 4;
            public const int BuildingName = 5;
            public const int StreetNo = 6;
            public const int Street = 7;
            public const int Suburb = 8;
            public const int City = 9;
            public const int Province = 10;
            public const int PostalCode = 11;
        }
    }
}
