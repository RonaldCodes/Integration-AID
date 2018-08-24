namespace RouteBuilderManipulator
{
    public class SiteData
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public SiteData LippysSite()
        {
            return new SiteData()
            {
                ClientId = "269",
                UserName = "0000000000269",
                PassWord = "NiyeGmM4"
            };
        }

        public SiteData TrackmaticTest()
        {
            return new SiteData()
            {
                ClientId = "110",
                UserName = "000000000110",
                PassWord = "!J5GrvNP"
            };
        }

        public SiteData TrackmaticEastLondon()
        {
            return new SiteData()
            {
                ClientId = "454",
                UserName = "9408065009082",
                PassWord = "yase191!"
            };
        }
    }
}
