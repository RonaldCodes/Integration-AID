using System;

namespace Agent.Exceptions
{
    public class ClientNotSupportedException : Exception
    {
        public ClientNotSupportedException(string message) : base(message)
        {

        }
    }
}
