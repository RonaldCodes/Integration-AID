namespace Agent.Configuration
{
    public class ClientId
    {
        public string Value { get; }

        public ClientId(string value)
        {
            Value = value;
        }

        public static implicit operator ClientId(string clientId)
        {
            if (clientId == null)
            {
                return null;
            }

            return new ClientId(clientId);
        }

        public static implicit operator string(ClientId clientId)
        {
            return clientId?.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
