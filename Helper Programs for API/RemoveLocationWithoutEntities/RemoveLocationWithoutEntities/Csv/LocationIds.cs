using System;
using Agent.Exceptions;

namespace Agent.Csv
{
    public class LocationIds
    {
        private readonly Line _source;
        private const int expectedLength = 1;

        public LocationIds(Line source)
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

        public string GetLocationId()
        {
            return _source.GetStringByIndex(Constants.LocationId);
        }

        private static class Constants
        {
            public const int LocationId = 0;
        }
    }
}
