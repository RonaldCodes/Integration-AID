namespace UploadContacts.Site
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

        public SiteData CityLogistics()
        {
            return new SiteData
            {
                ClientId = "350",
                ProfileName = "CityLogistics"
            };
        }
        public SiteData BWH_Eastern_Region_Cluster()
        {
            return new SiteData
            {
                ClientId = "360",
                ProfileName = "Builders"
            };
        }
        public SiteData BWH_Western_Region_Cluster()
        {
            return new SiteData
            {
                ClientId = "465",
                ProfileName = "Builders-westcluster"
            };
        }
        public SiteData BWH_Bloemfontein_Region_Cluster()
        {
            return new SiteData
            {
                ClientId = "492",
                ProfileName = "Builders-BWH_Bloemfontein_Region_Cluster",
            };
        }
        public SiteData B_WC_Central_Cluster()
        {
            return new SiteData
            {
                ClientId = "490",
                ProfileName = "B_WC_Central_Cluster",
            };
        }
    }
}
