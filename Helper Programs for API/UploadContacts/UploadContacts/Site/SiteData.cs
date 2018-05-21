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
                ProfileName = "CityLogistics",
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
        public SiteData BWH_Eastern_Region_Cluster()
        {
            return new SiteData
            {
                ClientId = "360",
                ProfileName = "Builders",
                ClusterName = "BWH_Eastern_Region_Cluster",
                RouteTemplateId = "360/858b6de0-8d83-4f0b-9c94-5e61e7bc8565",
                DeliveryActionTypeId = "360/69a266a5-ffd0-4e9e-8f54-9a663dc688f1",
                CollectionActionTypeId = "",
                ServiceActionTypeId = "",
                UpliftActionTypeId = "",
                IBTActionTypeId = "",
                CTCActionTypeId = "",
                UserName = "",
                PassWord = ""
            };
        }
        public SiteData BWH_Western_Region_Cluster()
        {
            return new SiteData
            {
                ClientId = "465",
                ProfileName = "Builders-westcluster",
                ClusterName = "BWH_Western_Region_Cluster",
                RouteTemplateId = "360/858b6de0-8d83-4f0b-9c94-5e61e7bc8565",
                DeliveryActionTypeId = "360/69a266a5-ffd0-4e9e-8f54-9a663dc688f1",
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
