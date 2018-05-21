using System.Collections.Generic;
using System.Text.RegularExpressions;
using Trackmatic.Rest.Core.Model;
using UploadLocations.Csv;
using UploadLocations.Exceptions;
using UploadLocations.Site;

namespace UploadLocations.Transformers
{
    class LocationModelTransformer
    {
        private readonly List<LocationLines> _locations;
        private readonly SiteData _site;

        public LocationModelTransformer(List<LocationLines> locations, SiteData site)
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
                    Id = $"{_site.ClientId}/{RemoveIllegalChars(checkNull(""))}".Replace(" ", string.Empty),
                    Name = "",
                    Reference = "",
                    //Coords = new Trackmatic.Rest.SpecializedObservableCollection<OCoord>
                    //    {
                    //        new OCoord
                    //        {
                    //            Latitude = shipto.Position?.Latitude ?? 0.0,
                    //            Longitude = shipto.Position?.Longitude ?? 0.0,
                    //            Radius = 100
                    //        }
                    //    },
                    //Entrance = new OCoord
                    //{
                    //    Latitude = shipto.Position?.Latitude ?? 0.0,
                    //    Longitude = shipto.Position?.Longitude ?? 0.0
                    //},
                    Shape = EZoneShape.Radius,
                    ClientId = _site.ClientId,
                    IsActive = true,
                    StructuredAddress = new StructuredAddress
                    {
                        StreetNo = "",
                        Street = "",
                        Suburb = "",
                        City = "",
                        PostalCode = ""
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
