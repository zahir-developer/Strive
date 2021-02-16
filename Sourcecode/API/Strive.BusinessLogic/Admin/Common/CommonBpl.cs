using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO.User;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Location;
using Strive.Common;
using Strive.Crypto;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Common
{
    public class CommonBpl : Strivebase, ICommonBpl
    {
        private static Random random;


        public CommonBpl()
        {

        }

        public CommonBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result GetSearchResult<T>(string searchTerm)
        {
            ///...Yet to be implemented
            var res = new CommonRal(_tenant).DoSearch(searchTerm);
            return null;
        }
        public string RandomNumber(int length)
        {
            random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Result GetAllCodes()
        {
            try
            {
                var lstCode = new CommonRal(_tenant, false).GetAllCodes();
                _resultContent.Add(lstCode.WithName("Codes"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

        public Result GetCodesByCategory(GlobalCodes codeCategory)
        {
            try
            {
                var lstCode = new CommonRal(_tenant, false).GetCodeByCategory(codeCategory);
                _resultContent.Add(lstCode.WithName("Codes"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

        internal object GetGeocode(LocationAddress locationAddress)
        {
            string osmUri = "https://nominatim.openstreetmap.org/search?q=255+South%20Main%20Street,+Alpharetta+GA+30009&format=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(osmUri);
            request.Method = "GET";
            string apiResponse = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                apiResponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            List<Geocode> lstGeocode = JsonConvert.DeserializeObject<List<Geocode>>(apiResponse);
            return lstGeocode;
        }

        public Result GetCodesByCategory(int codeCategoryId)
        {
            try
            {
                var lstCode = new CommonRal(_tenant).GetCodeByCategoryId(codeCategoryId);
                _resultContent.Add(lstCode.WithName("Codes"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }


        public string GetApiResponse(string baseUrl, string apiMethod, string apiKey, string request)
        {
            string stringResult = string.Empty;
            var stringContent = new StringContent(request, UnicodeEncoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = client.PostAsync(apiMethod + "?apikey=" + apiKey, stringContent).Result;

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    stringResult = responseContent.ReadAsStringAsync().Result;
                }
            }
            return stringResult;
        }

        public async Task<Result> CreateLocationForWeatherPortal()
        {
            const string baseUrl = "https://api.climacell.co/";
            const string apiMethod = "v3/locations";
            const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";

            var weatherlocation = new WeatherLocation()
            {
                name = "Strive-Location1",
                point = new point() { lat = 34.07, lon = -84.29 }
            };
            var wlocation = JsonConvert.SerializeObject(weatherlocation);
            var stringContent = new StringContent(wlocation, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = await client.PostAsync(apiMethod + "?apikey=" + apiKey, stringContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }


        //https://api.climacell.co/v3/weather/forecast/daily?location_id=5efe1a24ed57b2001925dd6e&start_time=2020-07-02&end_time=2020-07-02&fields%5B%5D=precipitation&fields%5B%5D=precipitation_probability&fields%5B
        public async Task<Result> GetWeather()
        {
            const string baseUrl = "https://api.climacell.co/";
            const string apiMethod = "v3/weather/forecast/daily";
            const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";

            string location_id = "5efe1a24ed57b2001925dd6e";
            string start_time = "2020-07-02";
            string end_time = "2020-07-02";
            string[] fields = new string[] { "precipitation", "precipitation_probability", "temp" };

            var queries =
                $"location_id={location_id}&start_time={start_time}&end_time={end_time}&fields=precipitation&fields=precipitation_probability&fields=temp";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); ;

                // New code:
                var response = await client.GetAsync(apiMethod + "?apikey=" + apiKey + "&" + queries);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                }
            }

            return null;

        }



        public async Task<Result> GetAllWeatherLocations()
        {
            const string baseUrl = "https://api.climacell.co/";
            const string apiMethod = "v3/locations";
            const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = await client.GetAsync(apiMethod + "?apikey=" + apiKey);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                }
            }

            return null;

        }

        public bool VerifyEmail(string emailId)
        {
            return true;
        }

        public string RandomString(int length)
        {
            random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal Result ResetPassword(ResetPassword resetPassword)
        {
            ///...Hash the new password
            ///...Send OTP, Hashed Password, UserId to database to reset the password

            string newPass = Pass.Hash(resetPassword.NewPassword);
            string otp = resetPassword.OTP;
            string userId = resetPassword.UserId;
            var result = new CommonRal(_tenant, true).ResetPassword(userId, otp, newPass);

            _resultContent.Add((result).WithName("Status"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;
        }

        internal bool ForgotPassword(string userId)
        {
            ///... Generate OTP
            ///... Store in DB
            ///... Send Mail

            var otp = RandomString(4);
            new CommonRal(_tenant, true).SaveOTP(userId, otp);
            SendOtpEmail(userId, otp);
            return true;
        }

        public int CreateLogin(string emailId, string mobileNo)
        {
            string randomPassword = RandomString(6);

            string passwordHash = Pass.Hash(randomPassword);

            AuthMaster authMaster = new AuthMaster
            {
                UserGuid = Guid.NewGuid().ToString(),
                EmailId = emailId,
                MobileNumber = mobileNo,
                PasswordHash = passwordHash,
                SecurityStamp = "1",
                LockoutEnabled = 0,
                CreatedDate = DateTime.Now
            };
            var authId = new CommonRal(_tenant, true).CreateLogin(authMaster);

            if (authId > 0)
            {
                
                SendLoginCreationEmail(emailId, randomPassword);
            }

            return authId;
        }

        public bool Signup(UserSignupDto userSignup, Strive.BusinessEntities.Model.Client client)
        {
            var commonRal = new CommonRal(_tenant, true);
            AuthMaster authMaster = new AuthMaster
            {
                UserGuid = Guid.NewGuid().ToString(),
                EmailId = userSignup.EmailId,
                MobileNumber = userSignup.MobileNumber,
                SecurityStamp = "1",
                LockoutEnabled = 0,
                CreatedDate = DateTime.Now,
                UserType = userSignup.UserType,
                TenantId = userSignup.TenantId
            };
            string randomPassword = RandomString(6);
            var authId = commonRal.CreateLogin(authMaster);
            client.AuthId = authId;
            client.UpdatedDate = DateTime.Now;

            ///... Get the Tenant Connectionstring
            commonRal.UpdateClientAuth(client);
            return true;
        }

        public string GetUserSignupInviteCode(UserType userType, bool TenantHasClientData = false, int clientId = 0)
        {
            string invitationCode = $"{_tenant.TenatId},{userType},{TenantHasClientData.toInt()},{clientId}{DateTime.Today.ToString()}";
            string encryptedInvitationCode = Crypt.Encrypt(invitationCode);
            return encryptedInvitationCode;
        }

        public string GetDetailsFromInviteCode(string inviteCode)
        {
            string detail = Crypt.Decrypt(inviteCode);
            return detail;
        }



        public Result SendOTP(string emailId)
        {
            var otp = RandomString(4);
            new CommonRal(_tenant, true).SaveOTP(emailId, otp);
            return SendOtpEmail(emailId, otp);
        }

        public Result VerfiyOTP(string emailId, string otp)
        {
            int result = new CommonRal(_tenant, true).VerifyOTP(emailId, otp);
            _resultContent.Add((result > 0).WithName("Status"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;
        }
        public Result GetAllEmail()
        {
            try
            {
                var lstEmail = new CommonRal(_tenant).GetAllEmail();
                _resultContent.Add(lstEmail.WithName("EmailList"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public void SendLoginCreationEmail(string emailId, string defaultPassword)
        {
            SendMail(emailId, @"<p> Welcome " + emailId + @",</p>
            <p> You have successfully signed up with Strive.</p>
            <p> Your login Credentials:</p>
            <p> UserName: " + emailId + @".</p>
            <p> Password: " + defaultPassword + @".</p>
            <p> &nbsp;</p>
            <p> Thanks,</p>
            <p> Strive Team </p>", "Welcome to Strive");
        }

        public void SendHoldNotificationEmail(string emailId, string TicketNumber)
        {
            SendMail(emailId, @"<p>Hello " + emailId + @",</p>
            <p>Your Vehicle is on Hold.</p>
            <p> Ticketnumber: " + TicketNumber + @".</p>
            <p>Thanks,</p>
            <p>Strive Team.</p>
           ", "Vehicle is on Hold");
        }

        private Result SendOtpEmail(string emailId, string otp)
        {
            SendMail(emailId, @"<p>Hello " + emailId + @",</p>
            <p>Please use '" + otp + @"' as your confirm OTP to Reset your password.</p>
            <p>Thanks,</p>
            <p>Strive Team.</p>", "Strive OTP Verification");

            _resultContent.Add((true).WithName("Status"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;
        }

        public void SendMail(string email, string body, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", _tenant.FromMailAddress));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_tenant.SMTPClient, _tenant.Port.toInt(), false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("autonotify@telliant.com", _tenant.SMTPPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public Result GetEmailIdExist(string emailId)
        {
            return ResultWrap(new CommonRal(_tenant, true).GetEmailIdExist, emailId, "EmailIdExist");
        }
        public Result GetCityByStateId(int stateId)
        {
            return ResultWrap(new CommonRal(_tenant, false).GetCityByStateId, stateId, "cities");

        }

        public string GetMailContent (HtmlContent module , Dictionary<string,string>Name)
        {

            string subPath = string.Empty;
            //switch (module)
            //{
            //    case HtmlContent.EmployeeSignUp:
            //        subPath = _tenant.EmployeeSignUp;
            //        break;
            //    case HtmlContent.ClientSignUp:
            //        subPath = _tenant.EmployeeSignUp;
            //        break;
            //    case HtmlContent.HoldNotification:
            //        subPath = _tenant.EmployeeSignUp;
            //        break;

            //}

            //subPath = subPath.Replace("TENANT_NAME", _tenant.SchemaName);            

            StreamReader str = new StreamReader(subPath);
            string MailText = str.ReadToEnd();
           
            foreach (var item in Name)
            {
                MailText.Replace(item.Key, item.Value.Trim());                
            }
            str.Close();
            
            return MailText;
        }
    }
}
