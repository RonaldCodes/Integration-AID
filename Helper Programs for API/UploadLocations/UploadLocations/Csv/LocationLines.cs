using UploadLocations.Exceptions;

namespace UploadLocations.Csv
{
    public class LocationLines
    {
        private readonly Line _source;
        private const int ExpectedLength = 11;

        public LocationLines(Line source)
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
            return _source.GetStringByIndex(Constants.Name);
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

        private static class Constants
        {
            public const int Reference = 0;
            public const int Name = 1;
            public const int Address1 = 2;
            public const int Address2 = 3;
            public const int Address3 = 4;
        }
    }
}
