﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Strive.BusinessLogic.Location;
using System.Linq;
using Strive.BusinessEntities.Auth;
using Strive.Crypto;
using MimeKit;
using MailKit.Net.Smtp;
using Strive.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using MimeKit.Text;
using Strive.BusinessEntities.Model;

namespace Strive.BusinessLogic.Common
{
    public class CommonBpl : Strivebase, ICommonBpl
    {
        private static Random random;

        public CommonBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result GetSearchResult<T>(string searchTerm)
        {
            ///...Yet to be implemented
            var res = new CommonRal(_tenant).DoSearch(searchTerm);
            return null;
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

        public int CreateLogin(UserLogin userLogin)
        {
            string randomPassword = RandomString(6);
            bool isValidEmail = VerifyEmail(userLogin.EmailId);
            userLogin.PasswordHash = Pass.Hash(randomPassword);
            userLogin.EmailVerified = isValidEmail.toInt();
            userLogin.LockoutEnabled = 0;
            userLogin.UserGuid = Guid.NewGuid();
            var authId = new CommonRal(_tenant, true).CreateLogin(userLogin);
            SendLoginCreationEmail(userLogin.EmailId, randomPassword);
            return authId;
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

        private void SendLoginCreationEmail(string emailId, string defaultPassword)
        {
            SendMail(emailId, @"<p> Welcome " + emailId + @",</p>
            <p> You have successfully signed up with Strive.& nbsp;</p>
            <p> Your login Credentials:</p>
            <p> UserName: " + emailId + @".</p>
            <p> Password: " + defaultPassword + @".</p>
            <p> &nbsp;</p>
            <p> Thanks,</p>
            <p> Strive Team </p>", "Welcome to Strive");
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

        private void SendMail(string email, string body, string subject)
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


    }
}
