using System.Collections.Generic;

namespace ExtractDitAddresses.CSVReader
{
    public interface ICsvReader
    {
        IEnumerable<Line> Read(string content);
    }
}
