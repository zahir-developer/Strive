using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI.WebControls;


namespace MPOS
{
    public partial class Login : System.Web.UI.Page
    {

        string strConnString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        static SqlDataReader dr;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtUsername.Focus();

                HttpContext.Current.Session["txtUserName"] = "";
                HttpContext.Current.Session["txtPassword"] = "";
                HttpContext.Current.Session["LocationID"] = 0;
                HttpContext.Current.Session["LoginID"] = 0;
                HttpContext.Current.Session["Role"] = "";
                HttpContext.Current.Session["LocationDesc"] = "";

            }

        }
        protected void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            string strUsername = txtUsername.Text.Trim();
            string strPassword = txtPassword.Text.Trim();
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (strUsername.Length == 0)
            {
                // *** AlertBox ***//
                if (!cs.IsStartupScriptRegistered(cstype, key: "PopupScript"))
                {
                    String cstext = "alert('You must enter the Username field' );";
                    cs.RegisterStartupScript(cstype, key: "PopupScript", script: cstext, addScriptTags: true);
                }
                // *** end of line AlertBox ***//       
                return;
            }
            if (strPassword.Length == 0)
            {
                // *** AlertBox ***//
                if (!cs.IsStartupScriptRegistered(cstype, key: "PopupScript"))
                {
                    String cstext = "alert('You must enter the Password field!' );";
                    cs.RegisterStartupScript(cstype, key: "PopupScript", script: cstext, addScriptTags: true);
                }
                // *** end of line AlertBox ***//       
                return;
            }
            string loginID = "0";
            string locationID = "0";
            string role = "";
            string locationDesc = "";
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT top(1) SC.UserID, SC.LocationID, SC.Role, LM.LocationDesc FROM ASPNETSecurity as SC INNER JOIN LM_Locations as LM ON SC.LocationID = LM.LocationID WHERE (SC.UserName = @UserName) AND (SC.Password = @Password) And (SC.Active = 1)";
                cmd.Parameters.AddWithValue(parameterName: "@UserName", value: strUsername);
                cmd.Parameters.AddWithValue(parameterName: "@Password", value: strPassword);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    loginID = dr[0].ToString().Trim();
                    locationID = dr[1].ToString().Trim();
                    role = dr[2].ToString().Trim();
                    locationDesc = dr[3].ToString().Trim();
                }
                con.Close();
            }
            if (loginID == "0")
            {
                // *** AlertBox ***//
                if (!cs.IsStartupScriptRegistered(cstype, key: "PopupScript"))
                {
                    String cstext = "alert('Invalid Login please re-enter!' );";
                    cs.RegisterStartupScript(cstype, key: "PopupScript", script: cstext, addScriptTags: true);
                }
                // *** end of line AlertBox ***//       
                return;
            }
            else
            {

                HttpContext.Current.Session["LoginID"] = loginID;
                HttpContext.Current.Session["LocationID"] = locationID;
                HttpContext.Current.Session["Role"] = role;
                HttpContext.Current.Session["LocationDesc"] = locationDesc;

                if (!cs.IsStartupScriptRegistered(cstype, key: "PopupScript"))
                {
                    String cstext = "alert('Login OK!' );";
                    cs.RegisterStartupScript(cstype, key: "PopupScript", script: cstext, addScriptTags: true);
                }
                Response.Redirect(url: "~/default.aspx");

            }

        }


    }
}