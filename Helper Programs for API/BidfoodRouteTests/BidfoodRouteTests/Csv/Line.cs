using Agent.Exceptions;

namespace Agent.Csv
{
    public class Line
    {
        public Line(string[] data, int lineNumber)
        {
            Data = data;
            LineNumber = lineNumber;
        }

        public string[] Data { get; private set; }

        public int LineNumber { get; private set; }

        public string Get(int index)
        {
            if (index > Data.Length - 1)
            {
                throw new InvalidIndexException(index, this);
            }

            return Data[index];
        }
    }
}
