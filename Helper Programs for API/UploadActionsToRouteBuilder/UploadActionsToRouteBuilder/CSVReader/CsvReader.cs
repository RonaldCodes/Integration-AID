using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Agent.Csv
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

        public IEnumerable<Line> Read(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Read(reader.ReadToEnd());
            }
        }

        public IEnumerable<Line> Read(string content)
        {
            var lineNumber = 0;

            foreach (var data in GetLines(content))
            {
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }

                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                using (var parser = new TextFieldParser(stream))
                {
                    parser.SetDelimiters(_configuration.Delimeter);
                    var fields = parser.ReadFields();
                    yield return new Line(fields, ++lineNumber);

                }
            }
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
