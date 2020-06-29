using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MPOS
{
    public partial class EODScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if (HttpContext.Current.Session["LoginID"] == null || HttpContext.Current.Session["LoginID"].ToString() == "0")
                    {
                        Response.Redirect(url: "~/SessionExpired.aspx");
                    }
                    ServerName.Text = Request.ServerVariables["SERVER_NAME"].ToLower().Trim();
                    LocationDesc.Text = HttpContext.Current.Session["LocationDesc"].ToString();
                    LocationID.Text = HttpContext.Current.Session["LocationID"].ToString();
                    LoginID.Text = HttpContext.Current.Session["LoginID"].ToString();
                    Role.Text = HttpContext.Current.Session["Role"].ToString();
                    RedirectUrl.Text = ConfigurationManager.AppSettings["RedirectUrl"].ToString();
                }
            }

        }
    }
}