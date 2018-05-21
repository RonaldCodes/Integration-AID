using System.Collections.Generic;
using Agent.Model;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest;

namespace Agent.Integration
{
    public class Sites
    {
        private readonly IDictionary<string, RttSiteDefault> _data;
        private readonly string _bu;
        public readonly string siteId;

        public Sites(string bu) : base()
        {
            _bu = bu;
            siteId = ResolveBusinessUnit();
        }

        public Sites()
        {
            _data = new Dictionary<string, RttSiteDefault>
            {
                {
                    "331", new RttSiteDefault
                    {
                        Id = "331",
                        RttTemplateId = "331/99781316-3987-4d90-9b0d-67e8a68002a4",
                        RttActionTypeDelivery = "331/5118fb3b-7252-4792-a411-5931d60ad457",
                        RttActionTypeCollection = "331/d03780db-f113-4492-9c29-e7b43fbf3f21",
                        StaticDeco = new OLocation()
                        {
                            Id = "331/281ea35d-6fdf-42f2-a567-9cb571be31f4",
                            Name = "RTT JHB Depot",
                            ClientId = "331",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 28.230164051055908,
                                    Latitude = -26.162987816205614,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.231601715087887,
                                    Latitude = -26.165838141751987,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.238811492919922,
                                    Latitude = -26.16591517661071,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.23887586593628,
                                    Latitude = -26.16312263046987,
                                    Radius = 100
                                }
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -26.165626295628087,
                               Longitude = 28.233511447906494,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                { 
                    "334", new RttSiteDefault
                    {
                        Id = "334",
                        RttTemplateId = "334/f754ce40-afbd-4fff-9dfd-26b16300cede",
                        RttActionTypeDelivery = "334/9a3f0045-d530-4a2c-85a8-49af89b1dee5",
                        RttActionTypeCollection = "334/a4b3c4e3-80f5-4592-9325-8e6e81b73e39",
                        StaticDeco = new OLocation()
                        {
                            Id = "334/4a831a16-aeee-4402-b375-c429fb5d63c2",
                            Name = "RTT Cape Town Depot",
                            ClientId = "334",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 18.614450097084045,
                                    Latitude = -33.936964672204326,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 18.61704111099243,
                                    Latitude = -33.93530013091732,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 18.613409399986267,
                                    Latitude = -33.933559893871575,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 18.61248135566711,
                                    Latitude = -33.935122103185066,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 18.612636923789978,
                                    Latitude = -33.93587426781812,
                                    Radius = 100
                                }
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -33.935599,
                               Longitude = 18.613556,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "335", new RttSiteDefault
                    {
                        Id = "335",
                        RttTemplateId = "335/ac8eb4d1-c91b-4f9d-8f52-181d4b03531e",
                        RttActionTypeDelivery = "335/0bf3b06d-f1ca-4837-9e35-2972612099e4",
                        RttActionTypeCollection = "335/be709cf4-0374-4a20-b6c6-8f65d7b0f555",
                        StaticDeco = new OLocation()
                        {
                            Id = "335/655f133c-45ea-4310-a5c7-9e91de49ff0d",
                            Name = "RTT Durban Depot",
                            ClientId = "335",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 30.996272563934326,
                                    Latitude = -29.78251828718064,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 30.997034311294552,
                                    Latitude = -29.78225755812941,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 30.997340083122253,
                                    Latitude = -29.78031603651976,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 30.996283292770386,
                                    Latitude = -29.780078581652674,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 30.99414825439453,
                                    Latitude = -29.78121463513444,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 30.99395513534546,
                                    Latitude = -29.78266727490505,
                                    Radius = 100
                                },
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -29.782592,
                               Longitude = 30.995531,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "336", new RttSiteDefault
                    {
                        Id = "336",
                        RttTemplateId = "336/ba9e7cb2-e63a-4bab-a45f-9c217ec889f7",
                        RttActionTypeDelivery = "336/2f9f2969-30e6-41c6-a9c4-be7dacf99b18",
                        RttActionTypeCollection = "336/e3e65b86-c019-4529-a9d4-c8b0c26159bd",
                        StaticDeco = new OLocation()
                        {
                            Id = "336/b22e9bc6-e43e-48cc-a900-0efb2e7aecc8",
                            Name = "RTT Bloemfontein Depot",
                            ClientId = "336",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 26.22031509876251,
                                    Latitude = -29.14922380750669,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 26.22036874294281,
                                    Latitude = -29.15034821008464,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 26.22189223766327,
                                    Latitude = -29.15015612551587,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 26.22183322906494,
                                    Latitude = -29.149017665699127,
                                    Radius = 100
                                }
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -29.149397153706513,
                               Longitude = 26.221699118614197,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "337", new RttSiteDefault
                    {
                        Id = "337",
                        RttTemplateId = "337/696e5537-57d0-4947-b062-386a306d7e24",
                        RttActionTypeDelivery = "337/0670bed5-1288-4f94-bcf2-6edd086d0fd7",
                        RttActionTypeCollection = "337/6827be25-d7d0-4e1c-a509-666ac4f5d387",
                        StaticDeco = new OLocation()
                        {
                            Id = "337/cda440bc-29bc-4c7a-b8b9-bf52a86d3e85",
                            Name = "RTT East London Depot",
                            ClientId = "337",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 27.87680447101593,
                                    Latitude = -33.033006219910035,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 27.87757158279419,
                                    Latitude = -33.03267791671306,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 27.877153158187866,
                                    Latitude = -33.031953846434405,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 27.876471877098083,
                                    Latitude = -33.03216072426399,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 27.876418232917786,
                                    Latitude = -33.03230913632079,
                                    Radius = 100
                                },
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -33.03268691133749,
                               Longitude = 27.87732481956482,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "338", new RttSiteDefault
                    {
                        Id = "338",
                        RttTemplateId = "338/66d2edd5-10d0-4729-bc28-7b4631d0e710",
                        RttActionTypeDelivery = "338/814caa97-80f6-4ee7-9413-13e0b0b3fdcd",
                        RttActionTypeCollection = "338/d69807b8-e764-483b-9404-31f8d872cec9",
                        StaticDeco = new OLocation()
                        {
                            Id = "338/b47ab260-301a-4122-8348-2caa5989d469",
                            Name = "RTT - George Depot",
                            ClientId = "338",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 22.466434836387634,
                                    Latitude = -33.977717973369415,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 22.46555507183075,
                                    Latitude = -33.978790047964075,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 22.467041015625,
                                    Latitude = -33.97952403314717,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 22.467851042747498,
                                    Latitude = -33.97845641624045,
                                    Radius = 100
                                }
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -33.97863890471516,
                               Longitude =22.467078566551205,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "339", new RttSiteDefault
                    {
                        Id = "339",
                        RttTemplateId = "339/401ca3ab-d1b9-4a20-be4d-f8b5db3ec84a",
                        RttActionTypeDelivery = "339/a0a98523-e356-476b-a432-5b789c6f6788",
                        RttActionTypeCollection = "339/0056d557-6951-4aa4-9a08-92572fa7e4ae",
                        StaticDeco = new OLocation()
                        {
                            Id = "339/9d0cab26-0cba-479b-92ec-dc6e0c4e3554",
                            Name = "RTT - Port Elizabeth Depot",
                            ClientId = "339",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 25.616447925567627,
                                    Latitude = -33.89357360965879,
                                    Radius = 100
                                }
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -33.89366266698077,
                               Longitude = 25.616512298583984,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "347", new RttSiteDefault
                    {
                        Id = "347",
                        RttTemplateId = "347/319b62c0-8de8-4baa-ad82-46fe3168f752",
                        RttActionTypeDelivery = "347/85d01dbe-db72-46d6-94c9-9b22a3e5da7e",
                        RttActionTypeCollection = "347/bcad021c-eee3-4477-8c2e-7f888e82a393",
                        StaticDeco = new OLocation()
                        {
                            Id = "347/e35b0d5b-a9c7-4e60-969d-ddaa9b13058c",
                            Name = "RTT Polokwane Depot",
                            ClientId = "347",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 29.470538198947906,
                                    Latitude = -23.865105152142355,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 29.47132408618927,
                                    Latitude = -23.865563840182375,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 29.471656680107117,
                                    Latitude = -23.864877033956088,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 29.470878839492794,
                                    Latitude = -23.86448457162005,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 29.4698166847229,
                                    Latitude = -23.86395965138651,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 29.46902275085449,
                                    Latitude = -23.865240060558065,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 29.470197558403015,
                                    Latitude = -23.86572572969097,
                                    Radius = 100
                                },
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -23.864886,
                               Longitude = 29.469205,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "351", new RttSiteDefault
                    {
                        Id = "351",
                        RttTemplateId = "351/cdc56cc1-6349-48c9-892b-38facef37669",
                        RttActionTypeDelivery = "351/7b7e7866-f138-48db-be34-94f1355dc938",
                        RttActionTypeCollection = "351/989588c8-b269-4131-a7bf-12a6e7f43fb6",
                        StaticDeco = new OLocation()
                        {
                            Id = "351/4039fe1c-ba5c-4ebb-854a-202354a0b322",
                            Name = "RTT - Tarsus",
                            ClientId = "351",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 28.095903396606445,
                                    Latitude = -26.05597326173905,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.097459077835083,
                                    Latitude = -26.05387206849224,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.096354007720944,
                                    Latitude = -26.05325519726616,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.09619307518005,
                                    Latitude = -26.052619045413532,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.094390630722042,
                                    Latitude = -26.052416632736286,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.094336986541748,
                                    Latitude = -26.055549170723882,
                                    Radius = 100
                                },
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -26.052079277497377,
                               Longitude = 28.09802770614624,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "378", new RttSiteDefault
                    {
                        Id = "378",
                        RttTemplateId = "378/4d14ef5e-0a09-448b-bbb9-e3d4d9695400",
                        RttActionTypeDelivery = "378/9703c84e-39f3-45c5-a714-bb792a4b59e6",
                        RttActionTypeCollection = "378/84ea6c3d-a63c-4934-983a-78f8bc706c6c",
                        StaticDeco = new OLocation()
                        {
                            Id = "378/a57af681-8760-48b3-b3c3-162815c8703f",
                            Name = "Imperial Home Depot",
                            ClientId = "378",
                            Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 28.164407,
                                    Latitude = -25.888873,
                                    Radius = 100
                                }
                            },
                           Entrance = new OCoord()
                           {
                               Latitude = -25.88819086675887,
                               Longitude = 28.164511620998383,
                               Radius = 100
                           },
                           IsActive = true,
                           Shape = EZoneShape.Radius,
                           Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                        }
                    }
                },
                {
                    "394", new RttSiteDefault
                    {
                        Id = "394",
                        RttTemplateId = "394/2c354768-4035-4eeb-8bd6-bbe19b2c75fe",
                        RttActionTypeDelivery = "394/398d4e87-b074-432f-8a33-4082a35fd198",
                        RttActionTypeCollection = "394/7f7cd168-9a61-4cbf-b634-17f52bb3e96c",
                        StaticDeco = new OLocation()
                {
                    Id = "394/d2ad22b8-5826-45a6-a3fd-abb81c7129ae",
                    Name = "RTT - Jet Park",
                    ClientId = "394",
                    Coords = new SpecializedObservableCollection<OCoord>()
                    {
                        new OCoord()
                        {
                            Longitude = 28.231505155563354,
                            Latitude = -26.1658477711121,
                        },
                        new OCoord()
                        {
                            Longitude = 28.230292797088623,
                            Latitude = -26.163295962866382,
                        },
                        new OCoord()
                        {
                            Longitude = 28.23786735534668,
                            Latitude = -26.163199667122342,
                        },
                        new OCoord()
                        {
                            Longitude = 28.237717151641846,
                            Latitude = -26.16590554725614,
                        },
                    },
                    Entrance = new OCoord()
                    {
                        Longitude = 28.233511447906494,
                        Latitude = -26.165674442508227,
                    },
                    IsActive = true,
                    Shape = EZoneShape.Polygon,
                    Tags = new SpecializedObservableCollection<string>()
                           {
                               "Home Base"
                           }
                }
                    }
                },
                {
                    "396", new RttSiteDefault
                    {
                        Id = "396",
                        RttTemplateId = "396/de5b0d3b-64fd-4ad6-a8a6-20501112ea77",
                        RttActionTypeDelivery = "396/440672d9-f130-4439-8de5-52e417df1b9c",
                        RttActionTypeCollection = "396/81737144-d053-41e1-997c-2c839b8be918"
                    }
                },
                {
                    "401", new RttSiteDefault
                    {
                        Id = "401",
                        RttTemplateId = "401/766f802b-d8b8-43e3-8b5a-57adaad6f687",
                        RttActionTypeDelivery = "401/ed7218be-dbb5-4043-a516-3c326d72a23a",
                        RttActionTypeCollection = "401/fbfe105d-8d59-460c-849f-3b84fa6c24dd"
                    }
                }
            };
        }

        public RttSiteDefault Get(string buId)
        {
            var id = ResolveBusinessUnit(buId);           
            if (!_data.ContainsKey(id))
            {
                return null;
            }
            return _data[id];
        }

        public RttSiteDefault GetFromRouteId(int routeId)
        {
            var routeStore = new Routes();
            var routeBu = routeStore.Get(routeId);
            if (routeBu == null) return null;
            return Get(routeBu);
        }

        private string ResolveBusinessUnit(string buId)
        {
            var siteId = "";
            switch (buId)
            {
                case "21":
                    siteId = "331";
                    break;
                case "18":
                    siteId = "334";
                    break;
                case "19":
                    siteId = "335";
                    break;
                case "20":
                    siteId = "336";
                    break;
                case "9990":
                    siteId = "337";
                    break;
                case "100002":
                    siteId = "338";
                    break;
                case "100009":
                    siteId = "347";
                    break;
                case "2099":
                    siteId = "351";
                    break;
                case "2011":
                    siteId = "378";
                    break;
                case "2000":
                    siteId = "394";
                    break;
                case "4000":
                    siteId = "396";
                    break;
                case "8000":
                    siteId = "401";
                    break;
                case "6000":
                    siteId = "339";
                    break;
            }
            return siteId;
        }

        private string ResolveBusinessUnit()
        {
            var siteId = "";
            switch (_bu)
            {
                case "21":
                    siteId = "331";
                    break;
                case "18":
                    siteId = "334";
                    break;
                case "19":
                    siteId = "335";
                    break;
                case "20":
                    siteId = "336";
                    break;
                case "9990":
                    siteId = "337";
                    break;
                case "100002":
                    siteId = "338";
                    break;
                case "100009":
                    siteId = "347";
                    break;
                case "2099":
                    siteId = "351";
                    break;
                case "2011":
                    siteId = "378";
                    break;
                case "2000":
                    siteId = "394";
                    break;
                case "4000":
                    siteId = "396";
                    break;
                case "8000":
                    siteId = "401";
                    break;
                case "6000":
                    siteId = "339";
                    break;
            }
            return siteId;
        }
    }
}
