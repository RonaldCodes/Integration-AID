using Agent.Exceptions;
using System;
using System.Globalization;
using Trackmatic.Rest.Planning.Model;

namespace Agent.Csv
{
    public class ActionItem
    {
        private readonly Line _source;
        private const int ExpectedLength = 35;

        public ActionItem(Line source)
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
        public string GetActionReference()
        {
            return _source.GetStringByIndex(Constants.ActionReference);
        }
        public string GetCustomerName()
        {
            return _source.GetStringByIndex(Constants.CustomerName);
        }

        public string GetCustomerReference()
        {
            return _source.GetStringByIndex(Constants.CustomerReference);
        }

        public string GetInternalReference()
        {
            return _source.GetStringByIndex(Constants.InternalReference);
        }

        public string GetActionTypeName()
        {
            return _source.GetStringByIndex(Constants.ActionTypeName);
        }
        
        public DateTime GetExpectedDeliveryDate()
        {
            return _source.GetDateTimeByIndex(Constants.ExpectedDeliveryDate);
        }
        public string GetInstructions()
        {
            return _source.GetStringByIndex(Constants.Instructions);
        }

        public string GetSellToName()
        {
            return _source.GetStringByIndex(Constants.SellToName);
        }

        public string GetSellToReference()
        {
            return _source.GetStringByIndex(Constants.SellToReference);
        }

        public string GetIsAdhoc()
        {
            return _source.GetStringByIndex(Constants.IsAdhoc);
        }

        public int GetMeasure()
        {
            return _source.GetIntByIndex(Constants.Measure);
        }

        public string GetRestrictions()
        {
            return _source.GetStringByIndex(Constants.Restrictions);
        }

        public string GetShipToName()
        {
            return _source.GetStringByIndex(Constants.ShipToName);
        }

        public string GetShipToReference()
        {
            return _source.GetStringByIndex(Constants.ShipToReference);
        }
        public TimeSpan GetMaxStopTime()
        {
            var mst = _source.GetIntByIndex(Constants.MaxStopTime);
            return TimeSpan.FromMinutes(mst);
        }
        public double GetShipToLatitude()
        {
            var latitude = _source.GetStringByIndex(Constants.ShipToLatitude);
                if (!string.IsNullOrWhiteSpace(latitude))
                {
                    if (latitude != "0")
                    {
                        return double.Parse(latitude, CultureInfo.InvariantCulture);
                    }
                }
                return 0.0;
        }

        public double GetShipToLongitude()
        {
            var longitude = _source.GetStringByIndex(Constants.ShipToLongitude);
            if (!string.IsNullOrWhiteSpace(longitude))
            {
                if (longitude != "0")
                {
                    return double.Parse(longitude, CultureInfo.InvariantCulture);
                }
            }
            return 0.0;
        }
        public string GetShipToLongitude2()
        {
           return _source.GetStringByIndex(Constants.ShipToLongitude);
        }
        public string GetShipToLatitude2()
        {
            return _source.GetStringByIndex(Constants.ShipToLatitude);
        }
        public string GetUnitNo()
        {
            return _source.GetStringByIndex(Constants.UnitNo);
        }

        public string GetBuildingName()
        {
            return _source.GetStringByIndex(Constants.BuildingName);
        }
        public string GetStreetNo()
        {
            return _source.GetStringByIndex(Constants.StreetNo);
        }
        public string GetSubDivisionNumber()
        {
            return _source.GetStringByIndex(Constants.SubDivisionNumber);
        }
        public string GetStreet()
        {
            return _source.GetStringByIndex(Constants.Street);
        }
        public string GetSuburb()
        {
            return _source.GetStringByIndex(Constants.Suburb);
        }
        public string GetCity()
        {
            return _source.GetStringByIndex(Constants.City);
        }
        public string GetProvince()
        {
            return _source.GetStringByIndex(Constants.Province);
        }
        public string GetPostalCode()
        {
            return _source.GetStringByIndex(Constants.PostalCode);

        }
        public string GetMapCode()
        {
            return _source.GetStringByIndex(Constants.MapCode);

        }
        public double GetWeight()
        {
            return _source.GetDoubleByIndex(Constants.Weight);

        }
        public int GetPieces()
        {
            return _source.GetIntByIndex(Constants.Pieces);

        }
        public int GetPallets()
        {
            return _source.GetIntByIndex(Constants.Pallets);

        }
        public double GetVolumetricMass()
        {
            return _source.GetDoubleByIndex(Constants.VolumetricMass);

        }
        public double GetAmountEx()
        {
            return _source.GetDoubleByIndex(Constants.AmountEx);

        }
        public double GetAmountIncl()
        {
            return _source.GetDoubleByIndex(Constants.AmountIncl);

        }

        public EUnitOfMeasure DetermineMeasure()
        {
            var measure = GetMeasure();
            if (measure == 0)
            {
                return EUnitOfMeasure.Box;
            }
            else if (measure == 1)
            {
                return EUnitOfMeasure.Parcel;
            }
            else if (measure == 2)
            {
                return EUnitOfMeasure.Carton;
            }
            return EUnitOfMeasure.Box;
        }

        public EActionDirection DetermineDirection()
        {
            var method = GetActionTypeName().ToLower();
            if (method.Contains("delivery") || method.Contains("service"))
            {
                return EActionDirection.Outbound;
            }
            return EActionDirection.Inbound;
        }

        private static class Constants
        {
            public const int ActionReference = 0;
            public const int CustomerName = 1;
            public const int CustomerReference = 2;
            public const int InternalReference = 3;
            public const int ActionTypeName = 4;
            public const int ExpectedDeliveryDate = 5;
            public const int Instructions = 6;
            public const int SellToName = 7;
            public const int SellToReference = 8;
            public const int IsAdhoc = 9;
            public const int Measure = 10;
            public const int Restrictions = 11;
            public const int ShipToName = 12;
            public const int ShipToReference = 13;
            public const int MaxStopTime = 14;
            public const int ShipToLatitude = 15;
            public const int ShipToLongitude = 16;
            public const int UnitNo = 17;
            public const int BuildingName = 18;
            public const int StreetNo = 19;
            public const int SubDivisionNumber = 20;
            public const int Street = 21;
            public const int Suburb = 22;
            public const int City = 23;
            public const int Province = 24;
            public const int PostalCode = 25;
            public const int MapCode = 26;
            public const int Weight = 27;
            public const int Pieces = 28;
            public const int Pallets = 29;
            public const int VolumetricMass = 30;
            public const int AmountEx = 31;
            public const int AmountIncl = 32;
        }
    }
}
