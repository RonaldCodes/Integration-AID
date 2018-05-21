using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Model;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;
using Newtonsoft.Json;
using Agent.Integration;

namespace SoapXmlHandler
{
    class Program
    {
        static void Main(string[] args)
        {

            var jetParkSite = new RttSiteDefault
            {
                Id = "394",
                RttTemplateId = "394/2c354768-4035-4eeb-8bd6-bbe19b2c75fe",
                RttActionTypeDelivery = "394/398d4e87-b074-432f-8a33-4082a35fd198",
                RttActionTypeCollection = "394/7f7cd168-9a61-4cbf-b634-17f52bb3e96c",
                StaticDeco = new OLocation()
                {
                    Id = "",
                    Name = "",
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
            };

            var peSite = new RttSiteDefault
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
            };

            var trackmaticSite = new RttSiteDefault()
            {
                Id = "110",
                RttActionTypeCollection = "110/collection",
                RttActionTypeDelivery = "110/delivery",
                RttTemplateId = "110/6dae0856-0874-4ea9-8510-c7051347f2e7",
                StaticDeco = new OLocation()
                {
                    Id = "110/d7876ece-1ea1-4e1a-b0c4-a6375f1d1e1a",
                    Name = "RTT Tarsus",
                    ClientId = "110",
                    Coords = new SpecializedObservableCollection<OCoord>
                            {
                                new OCoord()
                                {
                                    Longitude = 28.09668123722076,
                                    Latitude = -26.051563604041345,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.09471786022186,
                                    Latitude = -26.05400218935181,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.096600770950314,
                                    Latitude = -26.05517808982661,
                                    Radius = 100
                                },
                                new OCoord()
                                {
                                    Longitude = 28.098574876785275,
                                    Latitude = -26.05237807790099,
                                    Radius = 100
                                }
                            },
                    Entrance = new OCoord()
                    {
                        Latitude = -26.052060000025836,
                        Longitude = 28.098086714744564,
                        Radius = 100
                    },
                    IsActive = true,
                    Shape = EZoneShape.Radius,
                    Tags = new SpecializedObservableCollection<string>()
                    {
                        "Home Base"
                    }
                }
            };

            var ihsSite = new RttSiteDefault()
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
            };

            var tarsusSite = new RttSiteDefault()
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
            };

            var cptSite = new RttSiteDefault
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
            };

            var styleSite = new RttSiteDefault
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
            };

            var uploadModelFactory = new DefaultUploadModelFactory();

            var uploadModelFactoryFactory = new UploadModelFactoryFactory();

            var tripDetails = JsonConvert.DeserializeObject<RTTTripDetails>(TestTrip.Trip_9000932);

            //var uploads = uploadModelFactory.Create(consignments, trackmaticSite).ToList();

            var factory = uploadModelFactoryFactory.Create(ihsSite.Id);

            var uploads = factory.Create(tripDetails, ihsSite);

            var json = uploads.ToJson(true);
            var json2 = uploads.Relationships.Select(x => x.ActionId).ToList().ToJson(true);

            //uploads.Route.Id = $"339/{Guid.NewGuid()}";

            var api = CreateLogin("378");

            //File.AppendAllLines("JsonTarsus.json",new[] { uploads.ToJson() });

            var asyncResponse = api.ExecuteRequest(new Upload(api.Context, uploads));

            //foreach (var item in uploads)
            //{
            //    var response = api.ExecuteRequest(new Upload(api.Context, item));
            //}
        }

        public static Api CreateLogin(string CustomerId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", CustomerId, "9210155014083");
            api.Authenticate("'P@ssword");
            return api;
        }

        public static Api CreateRTTLogin(string CustomerId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", CustomerId, "0000000000331");
            api.Authenticate("87rLYDMv");
            return api;
        }
    }
}
