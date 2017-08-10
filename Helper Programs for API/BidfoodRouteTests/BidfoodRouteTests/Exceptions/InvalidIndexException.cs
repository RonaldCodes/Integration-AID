using System;
using Agent.Csv;

namespace Agent.Exceptions
{
    public class InvalidIndexException : Exception
    {
        public InvalidIndexException(int index, Line line) : base($"Requested index {index} is invalid at line {line.LineNumber}")
        {
            
        }
    }
}
