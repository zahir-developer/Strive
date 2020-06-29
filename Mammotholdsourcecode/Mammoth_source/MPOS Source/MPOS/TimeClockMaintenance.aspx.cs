using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace MPOS
{


    public partial class TimeClockMaintenance : System.Web.UI.Page
    {
        string Connectionstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();

        protected void Page_Load(object sender, EventArgs e)
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
                Panel3.Visible = false;
                WeekOfLabel.Text = "Select Week";
            }
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            SheetID.Text = RadGrid1.SelectedValues["SheetID"].ToString();
            WeekOf.Text = RadGrid1.SelectedValues["WeekOf"].ToString();
            if (WeekOf.Text != null)
            {
                WeekOfLabel.Text = "Week of "+string.Format("{0:d}", WeekOf.Text).Replace("12:00:00 AM","");
            }
            
                UserID.Text = "";
            Panel3.Visible = true;
        }

        protected void RadGrid2_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserID.Text = RadGrid2.SelectedValues["UserID"].ToString();

        }
    }
}