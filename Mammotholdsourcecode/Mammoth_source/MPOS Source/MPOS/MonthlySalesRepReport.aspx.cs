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
    public partial class MonthlySalesRepReport : System.Web.UI.Page
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


                if (MonthlySalesReport.ReportSource.Parameters.Contains(name: "LocationID"))
                {
                    MonthlySalesReport.ReportSource.Parameters["LocationID"].Value = LocationID.Text;
                }
                else
                {
                    MonthlySalesReport.ReportSource.Parameters.Add(name: "LocationID", value: LocationID.Text);
                }

                if (MonthlySalesReport.ReportSource.Parameters.Contains(name: "rptmonth"))
                {
                    MonthlySalesReport.ReportSource.Parameters["rptmonth"].Value = DateTime.Now.ToString(format: "MM");
                }
                else
                {
                    MonthlySalesReport.ReportSource.Parameters.Add(name: "rptmonth", value: DateTime.Now.ToString(format: "MM"));
                }

                if (MonthlySalesReport.ReportSource.Parameters.Contains(name: "rptYear"))
                {
                    MonthlySalesReport.ReportSource.Parameters["rptYear"].Value = DateTime.Now.ToString(format: "yyyy");
                }
                else
                {
                    MonthlySalesReport.ReportSource.Parameters.Add(name: "rptYear", value: DateTime.Now.ToString(format: "yyyy"));
                }




            }

        }
    }
}