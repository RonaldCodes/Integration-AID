using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UploadAddressesToAVT
{
    public static class Utils
    {
        public static string CreateActionId(string clientId, string waybillReference)
        {
            return $"{clientId}/{waybillReference}";
        }
        public static string CreateActionTypeId(string clientId, string ActionType)
        {
            return $"{clientId}/{ActionType}";
        }

        public static string CreateEntityId(string clientId, string entityReference)
        {
            return $"{clientId}/entity/{entityReference}";
        }

        public static string CreateLocationId(string clientId, string decoReference, string actionTypeName)
        {
            switch (actionTypeName.ToLower())
            {
                case "delivery":
                    return $"{clientId}/$tmp/{decoReference}/D";

                case "collection":
                    return $"{clientId}/$tmp/{decoReference}/C";
                default:
                    return null;
            }
        }
        private static string ReplaceIllegalChars(string id)
        {
            foreach (var c in IllegalChars)
            {
                if (id.Contains(c))
                {
                    id = id.Replace(c, "_");
                }
            }
            return id;
        }
        public static string RemoveIllegalChars(string inputString)
        {
            var reg = new Regex(@"&+");
            var result = reg.Replace(inputString, "and");
            return result;
        }

        private static readonly List<string> IllegalChars = new List<string>()
        {
            "+",
            "%",
            "!"
        };

        public static DateTime SafeSqlDateTime(this DateTime datetime)
        {
            if (datetime == DateTime.MaxValue)
            {
                return DateTime.UtcNow;
            }

            if (datetime == DateTime.MinValue)
            {
                return DateTime.UtcNow;
            }

            return DateTime.SpecifyKind(datetime, DateTimeKind.Local).ToUniversalTime();
        }

        public static string AvtFromFancyAffairsId(string clientId, string value)
        {
            if (value.Any(char.IsLower))
            {
                return $"{clientId}/{value}:L";
            }

            return $"{clientId}/{value}";
        }
    }
}
