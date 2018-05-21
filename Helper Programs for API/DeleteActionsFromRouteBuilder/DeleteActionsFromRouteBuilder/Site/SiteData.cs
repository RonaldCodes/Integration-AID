namespace DeleteActionsFromRouteBuilder.Site
{
    public class SiteData
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public SiteData TrackmaticTest()
        {
            return new SiteData()
            {
                ClientId = "110",
                UserName = "000000000110",
                PassWord = "!J5GrvNP"
            };
        }
    }
}
