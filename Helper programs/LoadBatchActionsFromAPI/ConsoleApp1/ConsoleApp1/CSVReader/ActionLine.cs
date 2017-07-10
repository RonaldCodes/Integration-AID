using System;
using ExtractDitAddresses.Exceptions;

namespace ExtractDitAddresses.CSVReader
{
    public class ActionLine
    {
        private readonly Line _source;
        private const int expectedLength = 9;

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

        public string GetInternalActionId()
        {
            return _source.GetStringByIndex(Constants.InternalActionId);
        }

        public string GetReference()
        {
            return _source.GetStringByIndex(Constants.Reference);
        }

        public string GetInternalReference()
        {
            return _source.GetStringByIndex(Constants.InternalReference);
        }

        public string GetEntityDecoId()
        {
            return _source.GetStringByIndex(Constants.EntityDecoId);
        }

        public string GetEntityDecoReference()
        {
            return _source.GetStringByIndex(Constants.EntityDecoReference);
        }

        public string GetDateReference()
        {
            return _source.GetStringByIndex(Constants.DateReference);
        }

        public string GetInternalRouteId()
        {
            return _source.GetStringByIndex(Constants.InternalRouteId);
        }

        public string GetActualStart()
        {
            return _source.GetStringByIndex(Constants.ActualStart);
        }

        public string GetEntityDecoName()
        {
            return _source.GetStringByIndex(Constants.EntityDecoName);
        }

        private static class Constants
        {
            public const int InternalActionId = 0;
            public const int Reference = 1;
            public const int InternalReference = 2;
            public const int EntityDecoId = 3;
            public const int EntityDecoReference = 4;
            public const int EntityDecoName = 5;
            public const int DateReference = 6;
            public const int InternalRouteId = 7;
            public const int ActualStart = 8;
        }
    }
}
