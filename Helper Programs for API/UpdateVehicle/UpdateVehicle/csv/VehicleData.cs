using UpdateVehicle.Exceptions;

namespace UpdateVehicle.csv
{
    public class VehicleData
    {
        private readonly Line _source;
        private const int expectedLength = 5;

        public VehicleData(Line source)
        {
            if (source.Data != null)
            {
                if (source.Data.Length != expectedLength)
                {
                    throw new FieldFormatException(source.LineNumber, 0, $"Row has an invalid length expected {expectedLength} but was {source.Data.Length}");
                }
                _source = source;
            }
        }

        public string GetClientId()
        {
            return _source.GetStringByIndex(Constants.ClientId);
        }

        public string GetRegistration()
        {
            return _source.GetStringByIndex(Constants.Registration);
        }

        public string GetVehicleClass()
        {
            return _source.GetStringByIndex(Constants.VehicleClass);
        }

        public double GetVolume()
        {
            return _source.GetDoubleByIndex(Constants.Volume);
        }

        public int GetPallets()
        {
            return _source.GetIntByIndex(Constants.Pallets);
        }

        private static class Constants
        {
            public const int ClientId = 0;
            public const int Registration = 1;
            public const int VehicleClass = 2;
            public const int Volume = 3;
            public const int Pallets = 4;
        }
    }
}
