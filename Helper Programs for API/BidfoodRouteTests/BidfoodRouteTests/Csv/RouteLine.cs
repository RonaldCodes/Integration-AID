using Agent.Csv;
using Agent.Exceptions;

namespace BidfoodRouteTests.Csv
{
    public class RouteLine
    {
        private readonly Line _source;
        private const int expectedLength = 3;

        public RouteLine(Line source)
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

        public string GetAccNumber()
        {
            return _source.GetStringByIndex(Constants.AccNumber);
        }

        public string GetAccName()
        {
            return _source.GetStringByIndex(Constants.AccName);
        }

        public string GetSeq()
        {
            return _source.GetStringByIndex(Constants.Seq);
        }

        private static class Constants
        {
            public const int AccNumber = 0;
            public const int AccName = 1;
            public const int Seq = 2;
        }
    }
}
