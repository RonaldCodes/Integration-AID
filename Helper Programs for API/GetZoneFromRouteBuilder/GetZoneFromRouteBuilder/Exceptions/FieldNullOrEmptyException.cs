using System;

namespace GetZoneFromRouteBuilder
{
    public class FieldNullOrEmptyException : FormatException
    {
        public FieldNullOrEmptyException(int line, int index)
            : base($"Field {index} at line {line} cannot be null")
        {


        }
    }
}