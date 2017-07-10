using System;
using System.Globalization;
using System.Linq;
using ExtractDitAddresses.Exceptions;

namespace ExtractDitAddresses.CSVReader
{
    public static class Extensions
    {
        public static bool IsEmpty(this double[] coordinates)
        {
            return coordinates == null || !coordinates.Any() || coordinates.All(x => Math.Abs(x) <= 0);
        }

        public static double[] GetCoordinates(this Line line, int index)
        {
            var value = line.GetStringByIndexEnsureNotNull(index);

            var split = value.Split(' ');

            if (split.Length != 2)
            {
                throw new FieldFormatException(line.LineNumber, index, value);
            }

            return split.Select(x =>
            {
                try
                {
                    return double.Parse(x);
                }
                catch (Exception e)
                {
                    throw new FieldFormatException(line.LineNumber, index, value, e);
                }

            }).ToArray();
        }

        public static string GetStringByIndexEnsureNotNull(this Line line, int index)
        {
            return Trim(line.Get(index)).EnsureNotNull(line.LineNumber, index);
        }

        public static string GetStringByIndex(this Line line, int index)
        {
            if (line != null)
            {
                return Trim(line.Get(index));
            }
            return null;
        }

        public static DateTime GetDateTimeByIndex(this Line line, int index, string format)
        {
            if (line != null)
            {
                var value = GetStringByIndex(line, index);

                return DateTime.SpecifyKind(DateTime.ParseExact(value, format, null, DateTimeStyles.AssumeLocal), DateTimeKind.Local).ToUniversalTime();
            }
            return DateTime.Now;
        }

        public static DateTime GetSpecialDateTimeByIndex(this Line line, int index)
        {
            if (line != null)
            {
                var value = GetStringByIndex(line, index);
                return DateTime.SpecifyKind(DateTime.Parse(value, null, DateTimeStyles.AssumeLocal), DateTimeKind.Local).ToUniversalTime();
            }
            return DateTime.Now;
        }

        public static int GetIntByIndex(this Line line, int index)
        {
            var value = GetStringByIndex(line, index);
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static double GetDoubleByIndex(this Line line, int index)
        {
            var value = GetStringByIndex(line, index);
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static string EnsureNotNull(this string value, int line, int index)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new FieldNullOrEmptyException(line, index);
            }

            return value;
        }

        public static bool IsEmpty(this Line line, int index)
        {
            return string.IsNullOrEmpty(line.GetStringByIndex(index));
        }

        private static string Trim(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return value.Trim(' ').Replace("\"", string.Empty);
        }
    }
}
