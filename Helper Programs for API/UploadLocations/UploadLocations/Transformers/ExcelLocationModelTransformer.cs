using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Trackmatic.Rest.Core.Model;
using UploadLocations.Exceptions;
using UploadLocations.Site;

namespace UploadLocations.Transformers
{
    class ExcelLocationModelTransformer
    {
        private readonly List<ExcelModel> _locations;
        private readonly SiteData _site;

        public ExcelLocationModelTransformer(List<ExcelModel> locations, SiteData site)
        {
            _locations = locations;
            _site = site;
        }

        public List<OLocation> Transform()
        {
            var locationsToUpload = new List<OLocation>();

            foreach (var location in _locations)
            {
                locationsToUpload.Add(new OLocation
                {
                    Id = $"{_site.ClientId}/{RemoveIllegalChars(checkNull(location.Column2))}".Replace(" ", string.Empty),
                    Name = location.Column3,
                    Reference = location.Column2,
                    Coords = new Trackmatic.Rest.SpecializedObservableCollection<OCoord>
                        {
                            new OCoord
                            {
                                //Latitude = !String.IsNullOrWhiteSpace(location.Column8) ? Convert.ToDouble(location.Column8.Split(',')[0]): 0.0,
                                //Latitude = Convert.ToDouble(location.Column5),
                                 Latitude = 0.0,
                                //Longitude = !String.IsNullOrWhiteSpace(location.Column9) ? Convert.ToDouble(location.Column9.Split(',')[0]) : 0.0,
                                //Longitude = Convert.ToDouble(location.Column4),
                                 Longitude = 0.0,
                                Radius = 100
                            }
                        },
                    Entrance = new OCoord
                    {
                       // Latitude = !String.IsNullOrWhiteSpace(location.Column8) ?Convert.ToDouble(location.Column8.Split(',')[0]): 0.0,
                        //Latitude = Convert.ToDouble(location.Column5),
                         Latitude = 0.0,
                       // Longitude = !String.IsNullOrWhiteSpace(location.Column9) ? Convert.ToDouble(location.Column9.Split(',')[0]) : 0.0,
                        //Longitude = Convert.ToDouble(location.Column4),
                        Longitude = 0.0,
                    },
                    Shape = EZoneShape.Radius,
                    ClientId = _site.ClientId,
                    IsActive = true,
                    StructuredAddress = new StructuredAddress
                    {
                        //BuildingName = $"{location.Column3}",
                        StreetNo = $"{location.Column4}",
                        Street = $"{location.Column5}",
                        Suburb = $"{location.Column6}",
                        City = $"{location.Column7}",
                        PostalCode = $"{location.Column8}"
                    }
                });
            }
            return locationsToUpload;
        }

        public string RemoveIllegalChars(string inputString)
        {
            //var reg = new Regex("[^0-9a-zA-Z]");
            var reg = new Regex("[+!%-]");
            var result = reg.Replace(inputString, "_");
            return result;
        }

        public string checkNull(string value)
        {
            var unwantedChars = new Regex(@"\n\s+");
            var newValue = unwantedChars.Replace(value, "");
            if (string.IsNullOrEmpty(newValue))
            {
                 throw new ReferenceCannotBeNullException($"A reference within the route was null, please ensure that references are populated correctly.");
            }
            else
            {
                return value;
            }
        }
    }
}
