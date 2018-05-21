using UpdateADNonEntities.Exceptions;

namespace UpdateADNonEntities.Csv
{
    public class EntityLines
    {
        private readonly Line _source;
        private const int ExpectedLength = 3;

        public EntityLines(Line source)
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

        public string GetRef()
        {
            return _source.GetStringByIndex(Constants.Ref);
        }

        public string Getname()
        {
            return _source.GetStringByIndex(Constants.name);
        }

        public string Getemail()
        {
            return _source.GetStringByIndex(Constants.email);
        }

        private static class Constants
        {
            public const int Ref = 0;
            public const int name = 1;
            public const int email = 2;

        }
    }
}
