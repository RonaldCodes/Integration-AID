using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProfile
{
   public class SiteData
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public SiteData SelpalSite()
        {
            return new SiteData()
            {
                Id = "404",
                UserName = "00000000000404",
                PassWord = "tb!AEs8B"
            };
        }
    }
}
