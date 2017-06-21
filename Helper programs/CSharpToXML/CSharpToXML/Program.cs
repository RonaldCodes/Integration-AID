using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CSharpToXML
{
    public class Program
    {
        static void Main(string[] args)
        {
            var date = DateTime.Now;
            var route = new Route()
            {
                RouteName = "Zone02",
                Sequence = "1",
                StartTime = date,
                StartDate = date,
                ReferenceNo = "123",
                DriverName = "Freddy",
                VehicleRegistration = "FZ06MY GP",
                Customers = new List<Customer>
                {
                    new Customer()
                    {
                        DecoName = "Townsview Mall",
                        CustomerName = "Spar",
                        MaximumStopTime = new TimeSpan(0, 10, 0),
                        CellNo = "0113647894",
                        CustomerCode = "440336",
                        Email = "example@email.co.za",
                        CustomerReference = "spar5165516",
                        IsOnceOffDelivery = false,
                        Address = new Address
                            {
                                UnitNo = "7",
                                BuildingName = "Townsview Mall",
                                StreetNo = "51",
                                Street = "Rose Street",
                                SubdivisionNo = "",
                                Suburb = "Townsview",
                                City = "Johannesburg",
                                Province = "Gauteng",
                                PostalCode = "2190",
                                Latitude = -26.12759,
                                Longitude = 28.07323,
                            },
                        Actions = new List<Actions>
                        {
                            new Actions
                            {
                                CreatedOn = date,
                                Nature = "Product",
                                Reference = "cheese01",
                                InternalReference = "pickingSlip81",
                                ExpectedDeliveryDate = "5/30/2017",
                                IsCashOnDelivery = "No",
                                Unit = "2",
                                Weight = "12",
                                VolumetricMass = "43",
                                Pieces = "10",
                                Pallets = "1",
                                SpecialInstructions = "Please handle with care",
                                AmountIncl = "300",
                                AmountExcl = "258",
                                HandlingUnit = new List<HandlingUnit>
                                {
                                   new HandlingUnit()
                                   {
                                        CustomerReference = "CustRef101",
                                        Barcode = "102103659",
                                        Description = "Box of Feta",
                                        VolumetricMass = 6,
                                        Volume = 12,
                                        Weight = 15,
                                        Quantity = 150,
                                        Dimensions = new HandlingUnitDimensions()
                                        {
                                             Height = 12,
                                             Length = 8,
                                             Width = 10
                                        },
                                        UnitOfMeasure = EUnitOfMeasure.Box,
                                         
                                   }
                                }
                            }
                        }
                    }
                },
            };

            XmlSerializer xs = new XmlSerializer(typeof(Route));
            TextWriter tw = new StreamWriter(@".\XMLRouteTemplate.xml");
            xs.Serialize(tw, route);
        }
    }
}
