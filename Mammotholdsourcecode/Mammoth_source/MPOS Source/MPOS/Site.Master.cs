using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace MPOS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Handle the session timeout 
            string sessionExpiredUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "~/SessionExpired.aspx";
            var script = new StringBuilder();
            script.Append(value: "function expireSession(){ \n");
            script.Append(string.Format(format: " window.location = '{0}';\n", arg0: sessionExpiredUrl));
            script.Append(value: "} \n");
            script.Append(string.Format(format: "setTimeout('expireSession()', {0}); \n", arg0: this.Session.Timeout * 60000)); // Convert minutes to milliseconds 
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), key: "expirescript", script: script.ToString(), addScriptTags: true);

            if (HttpContext.Current.Session["LoginID"] == null || HttpContext.Current.Session["LoginID"].ToString() == "0")
            {
                Response.Redirect(url: "~/login.aspx");
            }
            ServerName.Text = Request.ServerVariables["SERVER_NAME"].ToLower().Trim();
            LocationDesc.Text = HttpContext.Current.Session["LocationDesc"].ToString();
            LocationID.Text = HttpContext.Current.Session["LocationID"].ToString();
            LoginID.Text = HttpContext.Current.Session["LoginID"].ToString();
            Role.Text = HttpContext.Current.Session["Role"].ToString();

            storeName.Text = LocationDesc.Text;

            if (Role.Text != "3")
            {

                link10URL.NavigateUrl = "~/Washes.aspx";
                link20URL.NavigateUrl = "~/Details.aspx";
                link30URL.NavigateUrl = "~/Sales.aspx";

                link40URL.NavigateUrl = "~/DailyTipReport.aspx";
                link41URL.NavigateUrl = "~/MonthlyTip.aspx";
                link42URL.NavigateUrl = "~/EndOfDay.aspx";
                link43URL.NavigateUrl = "~/WeeklySales.aspx";
                link52URL.NavigateUrl = "~/MonthlySalesRepReport.aspx";
                link44URL.NavigateUrl = "~/MonthlyCustomerSummary.aspx";
                link45URL.NavigateUrl = "~/MonthlyCustomerDetailReport.aspx";
                link46URL.NavigateUrl = "~/EODReport.aspx";
                //link48URL.NavigateUrl = "~/EODScreen.aspx";
                link53URL.NavigateUrl = "~/TimeSheet.aspx";
                link51URL.NavigateUrl = "~/TimeSummary.aspx";
                link54URL.NavigateUrl = "~/HourlyWashReport.aspx";

                //link50URL.NavigateUrl = "~/TimeClock.aspx";
                link50URL.NavigateUrl = "~/TimeLogin.aspx";

                link60URL.NavigateUrl = "~/Employees.aspx";
                link61URL.NavigateUrl = "~/TimeClockMaintenance.aspx";
                link62URL.NavigateUrl = "~/Clients.aspx";
                link63URL.NavigateUrl = "~/Vehicles.aspx";
                link64URL.NavigateUrl = "~/GiftCards.aspx";
                link65URL.NavigateUrl = "~/CashDrawerSetUp.aspx";
                link66URL.NavigateUrl = "~/CloseOutRegister.aspx";
            }
            else
            {
                link10URL.Visible = false;
                link20URL.Visible = false;
                link30URL.Visible = false;

                ReportsMenu.InnerHtml = "";

                link40URL.Visible = false;
                link41URL.Visible = false;
                link42URL.Visible = false;
                link43URL.Visible = false;
                link52URL.Visible = false;
                link44URL.Visible = false;
                link45URL.Visible = false;
                link46URL.Visible = false;
                //link48URL.Visible = false;
                link53URL.Visible = false;
                link51URL.Visible = false;
                link54URL.Visible = false;

                link50URL.NavigateUrl = "~/TimeLogin.aspx";

                AdminMenu.InnerHtml = "";


                link60URL.Visible = false;
                link61URL.Visible = false;
                link62URL.Visible = false;
                link63URL.Visible = false;
                link64URL.Visible = false;
                link65URL.Visible = false;
                link66URL.Visible = false;
            }

            link90URL.NavigateUrl = "~/login.aspx";

        }
    }
}