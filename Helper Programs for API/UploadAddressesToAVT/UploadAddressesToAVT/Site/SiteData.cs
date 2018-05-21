namespace UploadAddressesToAVT.Site
{
    public class SiteData
    {
        public string ClientId { get; set; }
        public string ProfileName { get; set; }
        public string RouteTemplateId { get; set; }
        public string DeliveryActionTypeId { get; set; }
        public string CollectionActionTypeId { get; set; }
        public string ServiceActionTypeId { get; set; }
        public string UpliftActionTypeId { get; set; }
        public string IBTActionTypeId { get; set; }
        public string CTCActionTypeId { get; set; }
        public string ClusterName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public SiteData RTT()
        {
            return new SiteData
            {
                ClientId = "351",
                ProfileName = "RTT",
                ClusterName = "",
                RouteTemplateId = "",
                DeliveryActionTypeId = "",
                CollectionActionTypeId = "",
                ServiceActionTypeId = "",
                UpliftActionTypeId = "",
                IBTActionTypeId = "",
                CTCActionTypeId = "",
                UserName = "",
                PassWord = ""
            };
        }
    }
}
