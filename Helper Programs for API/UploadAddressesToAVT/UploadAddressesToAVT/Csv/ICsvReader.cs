using System.Collections.Generic;
using System.IO;

namespace UploadAddressesToAVT.Csv
{
    public interface ICsvReader
    {
        IEnumerable<Line> Read(string content);
        IEnumerable<Line> Read(Stream stream);
    }
}
