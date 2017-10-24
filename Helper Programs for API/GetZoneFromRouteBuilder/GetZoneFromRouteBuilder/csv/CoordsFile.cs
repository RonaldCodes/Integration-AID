
namespace GetZoneFromRouteBuilder
{
    public class CoordsFile
    {
        private readonly Line _source;
        private const int ExpectedLength = 3;

        public CoordsFile(Line source)
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

        public string GetCoord1()
        {
            return _source.GetStringByIndex(Constants.Coord1);
        }

        public string GetCoord2()
        {
            return _source.GetStringByIndexEnsureNotNull(Constants.Coord2);
        }


        private static class Constants
        {
            public const int Coord1 = 0;
            public const int Coord2 = 1;
        }
    }
}
