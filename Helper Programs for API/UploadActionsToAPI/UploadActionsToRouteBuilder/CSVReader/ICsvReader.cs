using System.Collections.Generic;
using System.IO;

namespace Agent.Csv
{
    public interface ICsvReader
    {
        IEnumerable<Line> Read(string content);
        IEnumerable<Line> Read(Stream stream);
    }
}
