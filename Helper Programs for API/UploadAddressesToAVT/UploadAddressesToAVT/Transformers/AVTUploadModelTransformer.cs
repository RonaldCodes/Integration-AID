using System.Collections.Generic;
using Trackmatic.Rest.Core.Model;
using Trackmatic.Rest.Dit.Model;
using UploadAddressesToAVT.Csv;
using UploadAddressesToAVT.Site;

namespace UploadAddressesToAVT.Transformers
{
    public class AVTUploadModelTransformer
    {
        private readonly SiteData _site;
        private readonly List<AddressLines> _addresses;

        public AVTUploadModelTransformer(SiteData site, List<AddressLines> addresses)
        {
            _site = site;
            _addresses = addresses;
        }

        public List<DitAddress> Transform()
        {
            var addressesToUpload = new List<DitAddress>();

            foreach (var address in _addresses)
            {
                addressesToUpload.Add(new DitAddress
                {
                    Id = Utils.AvtFromFancyAffairsId(_site.ClientId, address.GetReference()),
                    Reference = address.GetReference(),
                    Name = address.GetReference(),
                    Coordinates = new OCoord()
                    {
                        Latitude = address.GetLatitude(),
                        Longitude = address.GetLongitude(),
                        Radius = 100
                    },
                    Address = new StructuredAddress
                    {
                        UnitNo = address.GetUnitNo(),
                        BuildingName = address.GetBuildingName(),
                        StreetNo = address.GetStreetNo(),
                        Street = address.GetStreet(),
                        Suburb = address.GetSuburb(),
                        City = address.GetCity(),
                        Province = address.GetProvince(),
                        PostalCode = address.GetPostalCode(),
                    },
                    ClientId = _site.ClientId,
                    Status = EDitStatus.New,
                });
            }
            return addressesToUpload;
        }
    }
}
