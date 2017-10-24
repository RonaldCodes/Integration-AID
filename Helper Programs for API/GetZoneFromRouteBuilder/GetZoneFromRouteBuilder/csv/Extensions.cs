using System;
using System.Globalization;
using System.Linq;

namespace GetZoneFromRouteBuilder
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
            return Trim(line.Get(index));
        }

        public static DateTime GetDateTimeByIndex(this Line line, int index, string format)
        {
            var value = GetStringByIndex(line, index);

            try
            {
                return DateTime.SpecifyKind(DateTime.ParseExact(value, format, null, DateTimeStyles.AssumeLocal), DateTimeKind.Local).ToUniversalTime();
            }
            catch (Exception e)
            {
                throw new FieldFormatException(line.LineNumber, index, value, e);
            }
        }

        public static DateTime GetDateTimeByIndex(this Line line, int index)
        {
            var value = GetStringByIndex(line, index);

            try
            {
                return DateTime.SpecifyKind(DateTime.Parse(value, null, DateTimeStyles.AssumeLocal), DateTimeKind.Local).ToUniversalTime();
            }
            catch (Exception e)
            {
                throw new FieldFormatException(line.LineNumber, index, value, e);
            }
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
            return value.Trim(' ').Replace("\"",string.Empty);
        }
    }
}
