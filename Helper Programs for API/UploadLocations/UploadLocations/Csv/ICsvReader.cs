using System.Collections.Generic;
using System.IO;

namespace UploadLocations.Csv
{
    public interface ICsvReader
    {
        IEnumerable<Line> Read(string content);
        IEnumerable<Line> Read(Stream stream);
    }
}
