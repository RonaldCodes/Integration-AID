using System;

namespace UploadAddressesToAVT.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(string Id) : base ($"Invalid date with load id: {Id} has been supplied")
        {

        }
    }
}
