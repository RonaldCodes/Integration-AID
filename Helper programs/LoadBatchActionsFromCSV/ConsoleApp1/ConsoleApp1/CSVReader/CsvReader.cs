using ExtractDitAddresses.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace ExtractDitAddresses.CSVReader
{
    public class CsvReader : ICsvReader
    {
        private readonly CsvConfiguration _configuration;

        public CsvReader() : this(new CsvConfiguration())
        {

        }

        public CsvReader(CsvConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Line> Read(string content)
        {
            var lineNumber = 0;

            foreach (var data in GetLines(content))
            {
                if (data == null)
                {
                    throw new NoDataException();
                }

                var line = string.IsNullOrEmpty(data) ? "" : RemoveQuotesFromFields(data);

                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                using (var parser = new TextFieldParser(stream))
                {
                    parser.SetDelimiters(_configuration.Delimeter);                    
                    var fields = parser.ReadFields();
                    yield return new Line(fields, ++lineNumber);

                }
            }
        }

        private string RemoveQuotesFromFields(string line)
        {
            line = line.Trim();

            var concat = line[0].ToString();

            for(var i = 1;i<line.Length - 1;i++)
            {
                if (line[i] == '"')
                {
                    if (line[i - 1] == ',' || line[i + 1] == ',')
                    {
                        concat += line[i];
                    }
                }
                else
                {
                    concat += line[i];
                }

            }

            concat += line[line.Length - 1];

            return concat;
        }

        private IEnumerable<string> GetLines(string content)
        {
            var lines = content.Split('\n');
            if (_configuration.HasHeaders)
            {
                lines = lines.Skip(1).ToArray();
            }
            return lines;
        }
    }
}
