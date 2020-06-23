using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MPOS
{
    public partial class TimeSet : System.Web.UI.Page
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
                UserID.Text = Request["UserID"].ToString();

                DateTime now = DateTime.Now;
                txtCurrentTime.Text = now.ToString(format: "MM'/'dd'/'yyyy h':'mm tt");

                if (UserID.Text == null || UserID.Text == "0")
                {
                    Response.Redirect(url: "~/TimeLogin.aspx");
                }
                using (SqlConnection con = new SqlConnection(strConnString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT top(1) FirstName+' '+Lastname as Name FROM LM_Users WHERE UserID=@UserID and LocationID=@LocationID";
                    cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                    cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtCurrentUser.Text = dr[0].ToString().Trim();
                    }
                    con.Close();
                }
                string strMM = DateTime.Now.ToString(format: "MM");
                string strDD = DateTime.Now.ToString(format: "dd");
                string strYY = DateTime.Now.ToString(format: "yyyy");

                using (SqlConnection con = new SqlConnection(strConnString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT top(1) Caction,CType FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime  DESC";
                    cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                    cmd.Parameters.AddWithValue(parameterName: "@MM", value: strMM);
                    cmd.Parameters.AddWithValue(parameterName: "@DD", value: strDD);
                    cmd.Parameters.AddWithValue(parameterName: "@YY", value: strYY);
                    cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ClockStatus.Text = dr[0].ToString().Trim();
                            intClockType.Text = dr[1].ToString().Trim();
                        }
                    }
                    else
                    {
                        ClockStatus.Text = "1";
                        intClockType.Text = "1";
                    }
                    con.Close();
                }

                if (ClockStatus.Text == "0")
                {
                    txtCurrentStatus.Text = "Clocked In";
                    ClockBtn.Text = "Clock Out";
                    ClockType.Visible = false;
                    ClockTypeLabel.Visible = false;
                }
                else
                {
                    txtCurrentStatus.Text = "Clocked Out";
                    ClockBtn.Text = "Clock In";
                    ClockType.Visible = true;
                    ClockTypeLabel.Visible = true;
                }
                if (intClockType.Text != null)
                {
                    ClockType.Text = intClockType.Text;
                }
                else
                {
                    ClockType.Text = "1";
                }

                TimeSetInfo.Visible = true;
                TimeCardInfo.Visible = false;
                TimeDetailInfo.Visible = false;
                ClockInOutBtnDisplay.Visible = false;
                DetailButtonDisplay.Visible = true;
                TimeCardBtnDisplay.Visible = true;
            }


        }
 
        public void TimeCard()
        {
            DateTime now = DateTime.Now;
            var dayOfWeek = (int)now.DayOfWeek;
            DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek);
            dtSun1.InnerText = startOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            dtSun.InnerText = startOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            int cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: startOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: startOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: startOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strSunI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strSunO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strSunI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strSunO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strSunI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strSunO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strSunI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strSunO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtSunI1 = DateTime.Parse(dtSun.InnerText + " " + strSunI1.InnerText);
            DateTime dtSunO1 = DateTime.Parse(dtSun.InnerText + " " + strSunO1.InnerText);
            DateTime dtSunI2 = DateTime.Parse(dtSun.InnerText + " " + strSunI2.InnerText);
            DateTime dtSunO2 = DateTime.Parse(dtSun.InnerText + " " + strSunO2.InnerText);
            DateTime dtSunI3 = DateTime.Parse(dtSun.InnerText + " " + strSunI3.InnerText);
            DateTime dtSunO3 = DateTime.Parse(dtSun.InnerText + " " + strSunO3.InnerText);
            DateTime dtSunI4 = DateTime.Parse(dtSun.InnerText + " " + strSunI4.InnerText);
            DateTime dtSunO4 = DateTime.Parse(dtSun.InnerText + " " + strSunO4.InnerText);

            TimeSpan tsSun1 = (dtSunO1 - dtSunI1);
            TimeSpan tsSun2 = (dtSunO2 - dtSunI2);
            TimeSpan tsSun3 = (dtSunO3 - dtSunI3);
            TimeSpan tsSun4 = (dtSunO4 - dtSunI4);
            TimeSpan tsSun6 = (tsSun1 + tsSun2);
            TimeSpan tsSun7 = (tsSun3 + tsSun4);
            TimeSpan tsSun = (tsSun6 + tsSun7);
            strSunDA.InnerText = tsSun.ToString(format: @"h\:mm");

            double minSun = tsSun.TotalMinutes;
            double hoursSun = minSun / 60;

            strSunTO.InnerText = hoursSun.ToString(format: "00.00");
            //End of day

            DateTime monOfWeek = startOfWeek.AddDays(+1);
            dtMon.InnerText = monOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: monOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: monOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: monOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strMonI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strMonO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strMonI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strMonO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strMonI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strMonO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strMonI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strMonO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtMonI1 = DateTime.Parse(dtMon.InnerText + " " + strMonI1.InnerText);
            DateTime dtMonO1 = DateTime.Parse(dtMon.InnerText + " " + strMonO1.InnerText);
            DateTime dtMonI2 = DateTime.Parse(dtMon.InnerText + " " + strMonI2.InnerText);
            DateTime dtMonO2 = DateTime.Parse(dtMon.InnerText + " " + strMonO2.InnerText);
            DateTime dtMonI3 = DateTime.Parse(dtMon.InnerText + " " + strMonI3.InnerText);
            DateTime dtMonO3 = DateTime.Parse(dtMon.InnerText + " " + strMonO3.InnerText);
            DateTime dtMonI4 = DateTime.Parse(dtMon.InnerText + " " + strMonI4.InnerText);
            DateTime dtMonO4 = DateTime.Parse(dtMon.InnerText + " " + strMonO4.InnerText);

            TimeSpan tsMon1 = (dtMonO1 - dtMonI1);
            TimeSpan tsMon2 = (dtMonO2 - dtMonI2);
            TimeSpan tsMon3 = (dtMonO3 - dtMonI3);
            TimeSpan tsMon4 = (dtMonO4 - dtMonI4);
            TimeSpan tsMon6 = (tsMon1 + tsMon2);
            TimeSpan tsMon7 = (tsMon3 + tsMon4);
            TimeSpan tsMon = (tsMon6 + tsMon7);
            strMonDA.InnerText = tsMon.ToString(format: @"h\:mm");
            double minMon = (tsMon.TotalMinutes + tsSun.TotalMinutes);
            double hoursMon = minMon / 60;
            strMonTO.InnerText = hoursMon.ToString(format: "00.00");
            // *** AlertBox ***//
            //Type cstype = this.GetType();
            //ClientScriptManager cs = Page.ClientScript;
            //if (!cs.IsStartupScriptRegistered(cstype, key: "PopupScript"))
            //{
            //    String cstext = "alert('minTotal=>" + minTotal.ToString() +"<= minMon=>" + minMon.ToString() +" <= ')";
            //    cs.RegisterStartupScript(cstype, key: "PopupScript", script: cstext, addScriptTags: true);
            //}
            // *** end AlertBox ***//
            //End of day
            DateTime TueOfWeek = startOfWeek.AddDays(+2);
            dtTue.InnerText = TueOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: TueOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: TueOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: TueOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strTueI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strTueO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strTueI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strTueO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strTueI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strTueO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strTueI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strTueO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtTueI1 = DateTime.Parse(dtTue.InnerText + " " + strTueI1.InnerText);
            DateTime dtTueO1 = DateTime.Parse(dtTue.InnerText + " " + strTueO1.InnerText);
            DateTime dtTueI2 = DateTime.Parse(dtTue.InnerText + " " + strTueI2.InnerText);
            DateTime dtTueO2 = DateTime.Parse(dtTue.InnerText + " " + strTueO2.InnerText);
            DateTime dtTueI3 = DateTime.Parse(dtTue.InnerText + " " + strTueI3.InnerText);
            DateTime dtTueO3 = DateTime.Parse(dtTue.InnerText + " " + strTueO3.InnerText);
            DateTime dtTueI4 = DateTime.Parse(dtTue.InnerText + " " + strTueI4.InnerText);
            DateTime dtTueO4 = DateTime.Parse(dtTue.InnerText + " " + strTueO4.InnerText);

            TimeSpan tsTue1 = (dtTueO1 - dtTueI1);
            TimeSpan tsTue2 = (dtTueO2 - dtTueI2);
            TimeSpan tsTue3 = (dtTueO3 - dtTueI3);
            TimeSpan tsTue4 = (dtTueO4 - dtTueI4);
            TimeSpan tsTue6 = (tsTue1 + tsTue2);
            TimeSpan tsTue7 = (tsTue3 + tsTue4);
            TimeSpan tsTue = (tsTue6 + tsTue7);
            strTueDA.InnerText = tsTue.ToString(format: @"h\:mm");
            double minTue = (tsTue.TotalMinutes + tsMon.TotalMinutes + tsSun.TotalMinutes);
            double hoursTue = minTue / 60;
            strTueTO.InnerText = hoursTue.ToString(format: "00.00");
            //End of day

            DateTime WedOfWeek = startOfWeek.AddDays(+3);
            dtWed.InnerText = WedOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: WedOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: WedOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: WedOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strWedI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strWedO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strWedI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strWedO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strWedI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strWedO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strWedI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strWedO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtWedI1 = DateTime.Parse(dtWed.InnerText + " " + strWedI1.InnerText);
            DateTime dtWedO1 = DateTime.Parse(dtWed.InnerText + " " + strWedO1.InnerText);
            DateTime dtWedI2 = DateTime.Parse(dtWed.InnerText + " " + strWedI2.InnerText);
            DateTime dtWedO2 = DateTime.Parse(dtWed.InnerText + " " + strWedO2.InnerText);
            DateTime dtWedI3 = DateTime.Parse(dtWed.InnerText + " " + strWedI3.InnerText);
            DateTime dtWedO3 = DateTime.Parse(dtWed.InnerText + " " + strWedO3.InnerText);
            DateTime dtWedI4 = DateTime.Parse(dtWed.InnerText + " " + strWedI4.InnerText);
            DateTime dtWedO4 = DateTime.Parse(dtWed.InnerText + " " + strWedO4.InnerText);

            TimeSpan tsWed1 = (dtWedO1 - dtWedI1);
            TimeSpan tsWed2 = (dtWedO2 - dtWedI2);
            TimeSpan tsWed3 = (dtWedO3 - dtWedI3);
            TimeSpan tsWed4 = (dtWedO4 - dtWedI4);
            TimeSpan tsWed6 = (tsWed1 + tsWed2);
            TimeSpan tsWed7 = (tsWed3 + tsWed4);
            TimeSpan tsWed = (tsWed6 + tsWed7);
            strWedDA.InnerText = tsWed.ToString(format: @"h\:mm");
            double minWed = (tsWed.TotalMinutes + tsTue.TotalMinutes + tsMon.TotalMinutes + tsSun.TotalMinutes);
            double hoursWed = minWed / 60;
            strWedTO.InnerText = hoursWed.ToString(format: "00.00");
            //End of day

            DateTime ThuOfWeek = startOfWeek.AddDays(+4);
            dtThu.InnerText = ThuOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: ThuOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: ThuOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: ThuOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strThuI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strThuO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strThuI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strThuO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strThuI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strThuO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strThuI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strThuO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtThuI1 = DateTime.Parse(dtThu.InnerText + " " + strThuI1.InnerText);
            DateTime dtThuO1 = DateTime.Parse(dtThu.InnerText + " " + strThuO1.InnerText);
            DateTime dtThuI2 = DateTime.Parse(dtThu.InnerText + " " + strThuI2.InnerText);
            DateTime dtThuO2 = DateTime.Parse(dtThu.InnerText + " " + strThuO2.InnerText);
            DateTime dtThuI3 = DateTime.Parse(dtThu.InnerText + " " + strThuI3.InnerText);
            DateTime dtThuO3 = DateTime.Parse(dtThu.InnerText + " " + strThuO3.InnerText);
            DateTime dtThuI4 = DateTime.Parse(dtThu.InnerText + " " + strThuI4.InnerText);
            DateTime dtThuO4 = DateTime.Parse(dtThu.InnerText + " " + strThuO4.InnerText);

            TimeSpan tsThu1 = (dtThuO1 - dtThuI1);
            TimeSpan tsThu2 = (dtThuO2 - dtThuI2);
            TimeSpan tsThu3 = (dtThuO3 - dtThuI3);
            TimeSpan tsThu4 = (dtThuO4 - dtThuI4);
            TimeSpan tsThu6 = (tsThu1 + tsThu2);
            TimeSpan tsThu7 = (tsThu3 + tsThu4);
            TimeSpan tsThu = (tsThu6 + tsThu7);
            strThuDA.InnerText = tsThu.ToString(format: @"h\:mm");
            double minThu = (tsThu.TotalMinutes + tsWed.TotalMinutes + tsTue.TotalMinutes + tsMon.TotalMinutes + tsSun.TotalMinutes);
            double hoursThu = minThu / 60;
            strThuTO.InnerText = hoursThu.ToString(format: "00.00");
            //End of day

            DateTime FriOfWeek = startOfWeek.AddDays(+5);
            dtFri.InnerText = FriOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: FriOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: FriOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: FriOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strFriI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strFriO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strFriI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strFriO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strFriI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strFriO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strFriI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strFriO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtFriI1 = DateTime.Parse(dtFri.InnerText + " " + strFriI1.InnerText);
            DateTime dtFriO1 = DateTime.Parse(dtFri.InnerText + " " + strFriO1.InnerText);
            DateTime dtFriI2 = DateTime.Parse(dtFri.InnerText + " " + strFriI2.InnerText);
            DateTime dtFriO2 = DateTime.Parse(dtFri.InnerText + " " + strFriO2.InnerText);
            DateTime dtFriI3 = DateTime.Parse(dtFri.InnerText + " " + strFriI3.InnerText);
            DateTime dtFriO3 = DateTime.Parse(dtFri.InnerText + " " + strFriO3.InnerText);
            DateTime dtFriI4 = DateTime.Parse(dtFri.InnerText + " " + strFriI4.InnerText);
            DateTime dtFriO4 = DateTime.Parse(dtFri.InnerText + " " + strFriO4.InnerText);

            TimeSpan tsFri1 = (dtFriO1 - dtFriI1);
            TimeSpan tsFri2 = (dtFriO2 - dtFriI2);
            TimeSpan tsFri3 = (dtFriO3 - dtFriI3);
            TimeSpan tsFri4 = (dtFriO4 - dtFriI4);
            TimeSpan tsFri6 = (tsFri1 + tsFri2);
            TimeSpan tsFri7 = (tsFri3 + tsFri4);
            TimeSpan tsFri = (tsFri6 + tsFri7);
            strFriDA.InnerText = tsFri.ToString(format: @"h\:mm");
            double minFri = (tsFri.TotalMinutes + tsThu.TotalMinutes + tsWed.TotalMinutes + tsTue.TotalMinutes + tsMon.TotalMinutes + tsSun.TotalMinutes);
            double hoursFri = minFri / 60;
            strFriTO.InnerText = hoursFri.ToString(format: "00.00");
            //End of day

            DateTime SatOfWeek = startOfWeek.AddDays(+6);
            dtSat.InnerText = SatOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            dtSat1.InnerText = SatOfWeek.ToString(format: "MM'/'dd'/'yyyy");
            // start of day
            cnt = 1;
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONVERT(varchar(15),CAST(Cdatetime as TIME),100) FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime ASC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: SatOfWeek.ToString(format: "MM"));
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: SatOfWeek.ToString(format: "dd"));
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: SatOfWeek.ToString(format: "yyyy"));
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        switch (cnt)
                        {
                            case 1:
                                strSatI1.InnerText = dr[0].ToString();
                                break;
                            case 2:
                                strSatO1.InnerText = dr[0].ToString();
                                break;
                            case 3:
                                strSatI2.InnerText = dr[0].ToString();
                                break;
                            case 4:
                                strSatO2.InnerText = dr[0].ToString();
                                break;
                            case 5:
                                strSatI3.InnerText = dr[0].ToString();
                                break;
                            case 6:
                                strSatO3.InnerText = dr[0].ToString();
                                break;
                            case 7:
                                strSatI4.InnerText = dr[0].ToString();
                                break;
                            case 8:
                                strSatO4.InnerText = dr[0].ToString();
                                break;

                        }
                        cnt = cnt + 1;
                    }
                }
                con.Close();
            }
            DateTime dtSatI1 = DateTime.Parse(dtSat.InnerText + " " + strSatI1.InnerText);
            DateTime dtSatO1 = DateTime.Parse(dtSat.InnerText + " " + strSatO1.InnerText);
            DateTime dtSatI2 = DateTime.Parse(dtSat.InnerText + " " + strSatI2.InnerText);
            DateTime dtSatO2 = DateTime.Parse(dtSat.InnerText + " " + strSatO2.InnerText);
            DateTime dtSatI3 = DateTime.Parse(dtSat.InnerText + " " + strSatI3.InnerText);
            DateTime dtSatO3 = DateTime.Parse(dtSat.InnerText + " " + strSatO3.InnerText);
            DateTime dtSatI4 = DateTime.Parse(dtSat.InnerText + " " + strSatI4.InnerText);
            DateTime dtSatO4 = DateTime.Parse(dtSat.InnerText + " " + strSatO4.InnerText);

            TimeSpan tsSat1 = (dtSatO1 - dtSatI1);
            TimeSpan tsSat2 = (dtSatO2 - dtSatI2);
            TimeSpan tsSat3 = (dtSatO3 - dtSatI3);
            TimeSpan tsSat4 = (dtSatO4 - dtSatI4);
            TimeSpan tsSat6 = (tsSat1 + tsSat2);
            TimeSpan tsSat7 = (tsSat3 + tsSat4);
            TimeSpan tsSat = (tsSat6 + tsSat7);
            strSatDA.InnerText = tsSat.ToString(format: @"h\:mm");
            double minSat = (tsSat.TotalMinutes + tsFri.TotalMinutes + tsThu.TotalMinutes + tsWed.TotalMinutes + tsTue.TotalMinutes + tsMon.TotalMinutes + tsSun.TotalMinutes);
            double hoursSat = minSat / 60;
            strSatTO.InnerText = hoursSat.ToString(format: "00.00");
            //End of day



        }

        protected void TimeCardBtnClicked(object sender, EventArgs e)
        {
            TimeCard();
            TimeSetInfo.Visible = false;
            TimeCardInfo.Visible = true;
            TimeDetailInfo.Visible = false;
            ClockInOutBtnDisplay.Visible = true;
            DetailButtonDisplay.Visible = true;
            TimeCardBtnDisplay.Visible = false;
        }
        protected void DetailBtnClicked(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            var dayOfWeek = (int)now.DayOfWeek;
            DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek);

            TimeSetInfo.Visible = false;
            TimeCardInfo.Visible = false;
            TimeDetailInfo.Visible = true;
            ClockInOutBtnDisplay.Visible = true;
            DetailButtonDisplay.Visible = false;
            TimeCardBtnDisplay.Visible = true;
        }
        protected void ReturnBtnClicked(object sender, EventArgs e)
        {
            Response.Redirect(url: "~/TimeLogin.aspx");
        }
        protected void ClockInOutBtnClicked(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            txtCurrentTime.Text = now.ToString(format: "MM'/'dd'/'yyyy h':'mm tt");

            if (UserID.Text == null || UserID.Text == "0")
            {
                Response.Redirect(url: "~/TimeLogin.aspx");
            }
            string strMM = DateTime.Now.ToString(format: "MM");
            string strDD = DateTime.Now.ToString(format: "dd");
            string strYY = DateTime.Now.ToString(format: "yyyy");

            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT top(1) Caction,CType FROM dbo.timeClock WHERE userid=@UserID and (Month(Cdatetime) = @MM) and (Day(Cdatetime) = @DD) and (Year(Cdatetime) = @YY) and (LocationID=@LocationID) Order by Cdatetime  DESC";
                cmd.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                cmd.Parameters.AddWithValue(parameterName: "@MM", value: strMM);
                cmd.Parameters.AddWithValue(parameterName: "@DD", value: strDD);
                cmd.Parameters.AddWithValue(parameterName: "@YY", value: strYY);
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ClockStatus.Text = dr[0].ToString().Trim();
                        intClockType.Text = dr[1].ToString().Trim();
                    }
                }
                else
                {
                    ClockStatus.Text = "1";
                    intClockType.Text = "1";
                }
                con.Close();
            }

            if (ClockStatus.Text == "0")
            {
                txtCurrentStatus.Text = "Clocked In";
                ClockBtn.Text = "Clock Out";
                ClockType.Visible = false;
                ClockTypeLabel.Visible = false;
            }
            else
            {
                txtCurrentStatus.Text = "Clocked Out";
                ClockBtn.Text = "Clock In";
                ClockType.Visible = true;
                ClockTypeLabel.Visible = true;
            }
            if (intClockType.Text != null)
            {
                ClockType.Text = intClockType.Text;
            }
            else
            {
                ClockType.Text = "1";
            }
            TimeCard();
            TimeSetInfo.Visible = true;
            TimeCardInfo.Visible = false;
            TimeDetailInfo.Visible = false;
            ClockInOutBtnDisplay.Visible = false;
            DetailButtonDisplay.Visible = true;
            TimeCardBtnDisplay.Visible = true;
        }
        protected void ClockBtnClicked(object sender, EventArgs e)
        {
            string cAction = "0";

            if (ClockBtn.Text == "Clock In")
            {
                cAction = "0";
            }
            else
            {
                cAction = "1";
            }
            string intClockID = "";
            using (SqlConnection con = new SqlConnection(strConnString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Max(ISNULL(ClockID,0))+1 FROM dbo.timeClock WHERE  (LocationID=@LocationID)";
                cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        intClockID = dr[0].ToString().Trim();
                    }
                }
                else
                {
                    intClockID = "1";

                }
                con.Close();
            }
            using (SqlConnection con2 = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "insert into  dbo.TimeClock(LocationID, UserID, ClockID, Cdatetime, Caction, editby, editdate, Paid, CType) values(@LocationID, @UserID, @ClockID, @Cdatetime, @Caction, @UserID, @Cdatetime, 0, @CType)";
                    cmd2.Parameters.AddWithValue(parameterName: "@LocationID", value: LocationID.Text);
                    cmd2.Parameters.AddWithValue(parameterName: "@UserID", value: UserID.Text);
                    cmd2.Parameters.AddWithValue(parameterName: "@ClockID", value: intClockID);
                    cmd2.Parameters.AddWithValue(parameterName: "@Cdatetime", value: txtCurrentTime.Text);
                    cmd2.Parameters.AddWithValue(parameterName: "@Caction", value: cAction);
                    cmd2.Parameters.AddWithValue(parameterName: "@CType", value: ClockType.Text.Trim());
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                }
            }

            TimeSetInfo.Visible = false;
            TimeCard();
            TimeCardInfo.Visible = true;
            TimeDetailInfo.Visible = false;
            ClockInOutBtnDisplay.Visible = true;
            TimeCardBtnDisplay.Visible = false;
            if (ClockType.Text.Trim() == "9")
            {
                DetailButtonDisplay.Visible = true;
            }
            else
            {
                DetailButtonDisplay.Visible = false;
            }


        }



    }
}