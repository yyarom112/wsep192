using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using src.ServiceLayer;
namespace WebServices
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceLayer service = ServiceLayer.getInstance();
        }
    }
}