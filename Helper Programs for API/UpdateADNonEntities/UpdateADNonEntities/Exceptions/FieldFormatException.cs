using System;

namespace UpdateADNonEntities.Exceptions
{
    public class FieldFormatException : FormatException
    {
        public FieldFormatException(int line, int index, string value)
            : this(line, index, value, null)
        {


        }

        public FieldFormatException(int line, int index, string value, Exception e)
            : base($"Field {index} at line {line} is in the incorrect format {value}", e)
        {


        }
    }
}