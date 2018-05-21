using System;

namespace UploadLocations.Exceptions
{
    public class ReferenceCannotBeNullException : Exception
    {
        public ReferenceCannotBeNullException(string message) : base(message)
        {

        }
    }
}
