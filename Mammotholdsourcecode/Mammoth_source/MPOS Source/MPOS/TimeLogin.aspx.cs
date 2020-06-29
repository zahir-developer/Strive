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
    public partial class TimeLogin : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        static SqlDataReader dr;
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
                }


        }
        protected void LoginBtnClicked(object sender, EventArgs e)
        {
            string strUserID = "0";
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT userid FROM LM_Users WHERE LoginID=@tUserName and LocationID=@LocationID";
                cmd.Parameters.AddWithValue(parameterName: "@tUserName", value: tUserName.Text);
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strUserID = dr[0].ToString().Trim();
                }
                con.Close();
            }
            if (strUserID != "0")
            {
                Response.Redirect("TimePassword.aspx?UserID=" + strUserID);
            }
            else
            {
                tUserName.Text = "";
            }

        }
        protected void NumberBtn_Clicked(object sender, EventArgs e)
        {
            string strButton = ((Button)sender).Text;
            // *** AlertBox ***//
            //Type cstype = this.GetType();
            //ClientScriptManager cs = Page.ClientScript;
            //if (!cs.IsStartupScriptRegistered(cstype, key: "PopupScript"))
            //{
            //    String cstext = "alert('**** NumberBtn=>"+ strButton + "****' );";
            //    cs.RegisterStartupScript(cstype, key: "PopupScript", script: cstext, addScriptTags: true);
            //}
            // *** end of line AlertBox ***//
            tUserName.Text = tUserName.Text + strButton;


        }
        protected void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            tUserName.Text = tUserName.Text.Substring(startIndex: 0, length: tUserName.Text.Length - 1);
        }
        protected void ClearBtn_Clicked(object sender, EventArgs e)
        {
            // *** AlertBox ***//
            tUserName.Text = "";
        }
    }
}