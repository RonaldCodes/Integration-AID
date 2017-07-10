using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPersonnelfromApi
{
    class Utility
    {
        public static void Log(string filename, params object[] items)
        {
            var parts = items.Select(Wrap).ToArray();

            Append(filename, parts);

            Console.WriteLine(string.Join(",", parts));
        }
        public static FileLine WrapDouble(double value, string header)
        {
            return new FileLine { Value = Wrap((object)(int)value), Header = header };
        }

        public static FileLine Wrap(object value, string header)
        {
            return new FileLine { Value = Wrap(value), Header = header };
        }

        public static void Append(string file, IEnumerable<FileLine> line)
        {
            var local = line.ToList();
            if (!File.Exists(file))
            {
                File.AppendAllLines(file, new[] { string.Join(",", local.Select(x => x.Header)) });
            }
            Append(file, local.Select(x => x.Value));
        }

        public static void Append(string file, IEnumerable<string> line)
        {
            File.AppendAllLines(file, new[] { string.Join(",", line) });
        }

        public static void Write(string file, IEnumerable<string> line)
        {
            File.WriteAllLines(file, new[] { string.Join(",", line) });
        }

        public static string Wrap(object value)
        {
            return $"\"{value}\"";
        }
        public static string WrapDouble(double value)
        {
            return Wrap(value);
        }

        public static bool Confirm(string message)
        {
            Console.WriteLine(message + " (y/n)?");

            return Console.ReadLine() == "y";
        }

        public class FileLine
        {
            public string Header { get; set; }

            public string Value { get; set; }
        }
    }
}
