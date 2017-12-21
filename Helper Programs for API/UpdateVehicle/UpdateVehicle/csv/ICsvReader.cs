using System.Collections.Generic;

namespace UpdateVehicle.csv
{
    public interface ICsvReader
    {
        IEnumerable<Line> Read(string content);
    }
}
