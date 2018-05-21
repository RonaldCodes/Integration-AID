using Agent.Model;
using System.Collections.Generic;

namespace Agent.Integration
{
    public class Utils
    {
        public bool IsTarsusRoute(string id)
        {
            if (id == "351") return true;
            return false;
        }

        public bool IsImperialRoute(string id)
        {
            if (id == "378") return true;
            return false;
        }

        public string CreateActionId(Consignment consignment, RttSiteDefault site)
        {
            return $"{site.Id}/{consignment.ConsignmentID}";
        }

        public string CreateSellToId(Consignment consignment, RttSiteDefault site)
        {
            return $"{site.Id}/entity/{consignment.Account.AccountNo.Trim()}";
        }

        public string CreateShipToId(Address address, RttSiteDefault site)
        {
            return $"{site.Id}/entity/{address.Addr_id.GetValueOrDefault().ToString()}";
        }

        public string CreateDecoId(Consignment consignment, RttSiteDefault site)
        {
            return $"{site.Id}/{consignment.Address.Addr_id.GetValueOrDefault().ToString()}";
        }

        public string CreateAdhocDecoId(Consignment consignment, RttSiteDefault site)
        {
            return $"{site.Id}/$tmp/{consignment.Address.Addr_id.GetValueOrDefault().ToString()}";
        }

        public string ResolveDecoId(Consignment consignment, RttSiteDefault site)
        {
            if (PermanentSites.Contains(site.Id))
            {
                var decoId = CreateDecoId(consignment, site);
                return decoId;
            }
            else
            {
                var decoId = CreateAdhocDecoId(consignment, site);
                return decoId;
            }
        }

        public List<string> AdHocSites = new List<string>()
        {
            "394",
            "396",
            "401",
            "351",
            "336",
            "337",
            "338",
            "339",
            "340",
            "341",
            "342",
            "343",
            "344",
            "345",
            "346",
            "347",
            "348",
        };

        public List<string> PermanentSites = new List<string>()
        {
            "331",
            "334",
            "335",
            "378",
        };
    }
}
