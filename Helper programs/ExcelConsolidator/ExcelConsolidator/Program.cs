using System.Linq;
using System.IO;

namespace ExcelConsolidator
{
    public class Program
    {
        static void Main(string[] args)
        {

            var allCsv = Directory.EnumerateFiles(@"FilesToConsolidate", "*.csv", SearchOption.TopDirectoryOnly);
            string[] header = { File.ReadLines(allCsv.First()).First(l => !string.IsNullOrWhiteSpace(l)) };
            var mergedData = allCsv
                .SelectMany(csv => File.ReadLines(csv)
                    .SkipWhile(l => string.IsNullOrWhiteSpace(l)).Skip(1)); // skip header of each file
            // Writes to bin folder
            File.WriteAllLines(@"File.csv", header.Concat(mergedData));
        }
    }
}