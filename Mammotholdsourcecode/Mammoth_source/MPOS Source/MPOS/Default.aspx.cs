using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MPOS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["LoginID"] == null || HttpContext.Current.Session["LoginID"].ToString() == "0")
            {
                if (Request.Browser.IsMobileDevice)
                {
                    Response.Redirect(url: "~/IPOD_Login.aspx");
                }
                else
                {
                    Response.Redirect(url: "~/Login.aspx");
                }
            }

        }
    }
}