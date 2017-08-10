using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class _Default : Page
    {
        [Inject] // we can't use constructor injection for webforms, so use property injection
        public IMailSender mailSender { get;set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            mailSender.Send("test@test.com", "Hello world!");
        }
    }
}