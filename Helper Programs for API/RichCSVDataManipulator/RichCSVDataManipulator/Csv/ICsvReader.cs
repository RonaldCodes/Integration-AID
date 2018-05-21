using System.Collections.Generic;

namespace Agent.Csv
{
    public interface ICsvReader
    {
        IEnumerable<Line> Read(string content);
    }
}
