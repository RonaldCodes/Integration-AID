using System;

namespace Agent.Exceptions
{
    public class NoFactoryException : Exception
    {
        public NoFactoryException(string message) : base(message)
        {

        }
    }
}
