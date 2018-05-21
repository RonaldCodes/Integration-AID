using UploadLocations.Exceptions;

namespace UploadLocations.Csv
{
    public class LocationLines
    {
        private readonly Line _source;
        private const int ExpectedLength = 8;

        public LocationLines(Line source)
        {
            if (source.Data == null)
            {
                throw new FieldFormatException(source.LineNumber, 0, $"No data was present at line {source.LineNumber}");
            }

            if (source.Data.Length != ExpectedLength)
            {
                throw new FieldFormatException(source.LineNumber, 0, $"Row has an invalid length expected {ExpectedLength} but was {source.Data.Length}");
            }
            _source = source;
        }

        public string GetFirstName()
        {
            return _source.GetStringByIndex(Constants.FirstName);
        }

        public string GetLastName()
        {
            return _source.GetStringByIndex(Constants.LastName);
        }

        public string GetIdentityNumber()
        {
            return _source.GetStringByIndex(Constants.IdentityNumber);
        }

        public string GetEmail()
        {
            return _source.GetStringByIndex(Constants.Email);
        }

        public string GetDepartmentPosition()
        {
            return _source.GetStringByIndex(Constants.DepartmentPosition);
        }

        public string GetMobile()
        {
            return _source.GetStringByIndex(Constants.Mobile);
        }

        public string GetHomeTel()
        {
            return _source.GetStringByIndex(Constants.HomeTel);
        }

        public string GetWorkTel()
        {
            return _source.GetStringByIndex(Constants.WorkTel);
        }

        private static class Constants
        {
            public const int FirstName = 0;
            public const int LastName = 1;
            public const int IdentityNumber = 2;
            public const int Email = 3;
            public const int DepartmentPosition = 4;
            public const int Mobile = 5;
            public const int HomeTel = 6;
            public const int WorkTel = 7;
        }
    }
}
