using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Telerik.Reporting;
using Telerik.ReportViewer.WebForms;

namespace MPOS
{
    public partial class MonthlyCustomerDetailReport : System.Web.UI.Page
    {
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



                if (ReportViewer1.ReportSource.Parameters.Contains(name: "LocationID"))
                {
                    ReportViewer1.ReportSource.Parameters["LocationID"].Value = LocationID.Text;
                }
                else
                {
                    ReportViewer1.ReportSource.Parameters.Add(name: "LocationID", value: LocationID.Text);
                }

                if (ReportViewer1.ReportSource.Parameters.Contains(name: "Month"))
                {
                    ReportViewer1.ReportSource.Parameters["Month"].Value = DateTime.Now.ToString(format: "MM");
                }
                else
                {
                    ReportViewer1.ReportSource.Parameters.Add(name: "Month", value: DateTime.Now.ToString(format: "MM"));
                }

                if (ReportViewer1.ReportSource.Parameters.Contains(name: "Year"))
                {
                    ReportViewer1.ReportSource.Parameters["Year"].Value = DateTime.Now.ToString(format: "yyyy");
                }
                else
                {
                    ReportViewer1.ReportSource.Parameters.Add(name: "Year", value: DateTime.Now.ToString(format: "yyyy"));
                }



            }

        }
    }
}