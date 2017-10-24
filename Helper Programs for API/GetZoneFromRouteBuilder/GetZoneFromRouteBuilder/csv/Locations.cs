using Agent.Exceptions;

namespace Agent.Csv
{
    public class Locations
    {
        private readonly Line _source;
        private const int ExpectedLength = 8;

        public Locations(Line source)
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

        public string GetLocationDescription()
        {
            return _source.GetStringByIndex(Constants.LocationDescription);
        }

        public double GetLatitude()
        {
            return _source.GetDoubleByIndex(Constants.Latitude);
        }

        public double GetLongitude()
        {
            return _source.GetDoubleByIndex(Constants.Longitude);
        }

        public string GetLocationGroup()
        {
            return _source.GetStringByIndex(Constants.LocationGroup);
        }

        public string GetDefaultLoadingMinutes()
        {
            return _source.GetStringByIndex(Constants.DefaultLoadingMinutes);
        }

        public string GetDefaultOffloadingMinutes()
        {
            return _source.GetStringByIndex(Constants.DefaultOffloadingMinutes);
        }

        public string GetCompany()
        {
            return _source.GetStringByIndex(Constants.Company);
        }

        public string GetExternalReference()
        {
            return _source.GetStringByIndex(Constants.ExternalReference);
        }

        private static class Constants
        {
            public const int LocationDescription = 0;
            public const int Latitude = 1;
            public const int Longitude = 2;
            public const int LocationGroup = 3;
            public const int DefaultLoadingMinutes = 4;
            public const int DefaultOffloadingMinutes = 5;
            public const int Company = 6;
            public const int ExternalReference = 7;
        }
    }
}
