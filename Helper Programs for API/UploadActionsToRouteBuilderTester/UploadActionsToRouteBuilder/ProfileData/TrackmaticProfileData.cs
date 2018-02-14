using Trackmatic.Rest.Core;

namespace UploadActionsToRouteBuilder
{
    //Koloks confidential profile integration agent credentials
    public class TrackmaticProfileData
    {
        public string ClientId { get { return "224"; } }
        public string Username { get { return "0000000000224"; } }
        public string Password { get {return "UpZmPJGK"; } }
        public string Url { get { return "https://rest.trackmatic.co.za/api/v1"; } }
        public Api CreateLogin()
        {
            var api = new Api(Url, ClientId, Username);
            api.Authenticate(Password);
            return api;
        }
    }
}
