using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Routing.Model;
using UploadLocations.Exceptions;
using UploadLocations.Site;

namespace UploadLocations.Transformers
{
    class ExcelEntityModelTransformer
    {
        private readonly List<ExcelModel> _locations;
        private readonly SiteData _site;

        public ExcelEntityModelTransformer(List<ExcelModel> locations, SiteData site)
        {
            _locations = locations;
            _site = site;
        }

        public List<Entity> Transform()
        {
            var entitiesToUpload = new List<Entity>();

            foreach (var location in _locations)
            {
                entitiesToUpload.Add(new Entity
                {
                    Id = $"{_site.ClientId}/entity/{RemoveIllegalChars(checkNull(location.Column2))}".Replace(" ", string.Empty),
                    Name = location.Column3,
                    Reference = location.Column2,
                    Requirements = new ObservableCollection<EntityRequirement>
                    {
                        new RequireActionDebrief
                        {
                            Type = "Action Debrief"
                        },
                        new RequireSignature()
                    },
                    Decos = CreateDecoAlias(location),
                    Tags = CreateTags(location)
                });
            }
            return entitiesToUpload;
        }

        public ObservableCollection<EntityTag> CreateTags(ExcelModel location)
        {
            return new ObservableCollection<EntityTag>()
            {
                new EntityTag
                {
                     Id = $"{_site.ClientId}/{location.Column1}",
                     Name =  $"{location.Column1}"
                }
            };
        }

        public System.Collections.ObjectModel.ObservableCollection<DecoAlias> CreateDecoAlias(ExcelModel location)
        {
            var decoAlias = new System.Collections.ObjectModel.ObservableCollection<DecoAlias>();
            decoAlias.Add( new DecoAlias
            {
                Id = $"{_site.ClientId}/{RemoveIllegalChars(checkNull(location.Column2))}".Replace(" ", string.Empty),
                DecoId = $"{_site.ClientId}/{RemoveIllegalChars(checkNull(location.Column2))}".Replace(" ", string.Empty),
                DecoName = location.Column3,
                Name = location.Column3,
                Reference = location.Column2,
                Entrance = new OCoord
                {
                    //Latitude = !String.IsNullOrWhiteSpace(location.Column8) ? Convert.ToDouble(location.Column8.Split(',')[0]) : 0.0,
                    //Latitude = Convert.ToDouble(location.Column5),
                    Latitude = 0.0,
                    //Longitude = !String.IsNullOrWhiteSpace(location.Column9) ? Convert.ToDouble(location.Column9.Split(',')[0]) : 0.0,
                    //Longitude = Convert.ToDouble(location.Column4),
                    Longitude = 0.0,
                    Radius = 100
                },
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
            return decoAlias;
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
