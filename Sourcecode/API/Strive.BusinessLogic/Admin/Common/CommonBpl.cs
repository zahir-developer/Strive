using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.City;
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
using System.Net.Mail;

using Azure.Storage.Blobs;
using System.Security.Authentication;
using Strive.BusinessLogic.EmailHelper.Dto;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO;

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

        internal List<Geocode> GetGeocode(LocationAddress locationAddress)
        {
            string osmUri = _tenant.OSMUri + locationAddress.Address1 + "," + locationAddress.CityName + "+" + locationAddress.StateName + "+" + locationAddress.Zip
                + "&format=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(osmUri);
            request.Method = "GET";
            request.UserAgent = _tenant.UserAgent;
            string apiResponse = String.Empty;
            try
            {
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteUser(int authId)
        {
            new CommonRal(_tenant, true).DeleteUser(authId);
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

        public (int authId, string password) CreateLogin(UserType userType, string emailId, string mobileNo, string password = "")
        {
            string randomPassword = password;

            if (randomPassword == string.Empty)
                randomPassword = RandomString(6);

            string passwordHash = Pass.Hash(randomPassword);

            AuthMaster authMaster = new AuthMaster
            {
                UserGuid = Guid.NewGuid().ToString(),
                EmailId = emailId,
                MobileNumber = mobileNo,
                PasswordHash = passwordHash,
                UserType = (int)userType,
                SecurityStamp = "1",
                LockoutEnabled = 0,
                CreatedDate = DateTime.Now
            };
            var authId = new CommonRal(_tenant, true).CreateLogin(authMaster);

            return (authId, randomPassword);
        }

        public UserDetailsViewModel GetUserPassword(string email, UserType userType)
        {
            var commonRal = new CommonRal(_tenant, true);
            return  commonRal.GetUserPassword(email,userType);
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
            string invitationCode = $"{_tenant.TenantId},{userType},{TenantHasClientData.toInt()},{clientId}{DateTime.Today.ToString()}";
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
        public void SendLoginCreationEmail(HtmlTemplate htmlTemplate, string emailId, string defaultPassword)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("{{emailId}}", emailId);
            keyValues.Add("{{password}}", defaultPassword);

            string emailContent = GetMailContent(htmlTemplate, keyValues);

            if (emailContent == string.Empty)
            {
                foreach (var key in keyValues)
                {
                    emailContent += key.Key + ": " + key.Value + ";";
                }
            }

            SendMail(emailId, emailContent, "Welcome to Strive !!!");
        }

        public void SendEmail(HtmlTemplate htmlTemplate, string emailId, Dictionary<string, string> keyValues, string sub)
        {
            try
            {

                EmailSettingDto emailSettingDto = new EmailSettingDto();
                emailSettingDto.FromEmail = _tenant.FromMailAddress;
                emailSettingDto.UsernameEmail = _tenant.FromMailAddress;
                emailSettingDto.UsernamePassword = _tenant.SMTPPassword;
                emailSettingDto.PrimaryPort = _tenant.Port.toInt();
                emailSettingDto.PrimaryDomain = _tenant.SMTPClient;

                string emailContent = GetMailContent(htmlTemplate, keyValues);

                EmailHelper.EmailSender emailSender = new EmailHelper.EmailSender(emailSettingDto);
                emailSender.Initialize(sub, emailId, emailContent);

                //SendMail(emailId, emailContent, "Welcome to Strive !!!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendHoldNotificationEmail(HtmlTemplate htmlTemplate, string emailId, string ticketNumber)
        {

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("{emailId}", emailId);
            keyValues.Add("{ticketNumber}", ticketNumber);
            string emailContent = GetMailContent(htmlTemplate, keyValues);
            SendMail(emailId, emailContent, "Vehicle is on Hold");
        }
        public void SendProductThresholdEmail(HtmlTemplate htmlTemplate, string emailId, string productName)
        {

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("{{emailId}}", emailId);
            keyValues.Add("{{productName}}", productName);
            string emailContent = GetMailContent(htmlTemplate, keyValues);
            SendMail(emailId, emailContent, "The Product has reached its threshold Limit");
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
        public void SendMultipleMail(string email, string body, string subject)
        {

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_tenant.FromMailAddress); //From Email Id  
            mailMessage.Subject = subject; //Subject of Email  
            mailMessage.Body = body; //body or message of Email  
            mailMessage.IsBodyHtml = true;


            string[] BCCId = email.Split(',');
            foreach (string BCCEmail in BCCId)
            {
                mailMessage.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id  
            }
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();  // creating object of smptpclient  
            smtp.Host = _tenant.SMTPClient;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = _tenant.FromMailAddress;
            NetworkCred.Password = _tenant.SMTPPassword;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.EnableSsl = true;

            smtp.Port = _tenant.Port.toInt();

            smtp.Send(mailMessage); //sending Email  
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

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // Note: only needed if the SMTP server requires authentication
                try
                {
                    client.SslProtocols |= SslProtocols.Tls;
                    client.CheckCertificateRevocation = false;
                    client.Connect(_tenant.SMTPClient, _tenant.Port.toInt(), false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_tenant.FromMailAddress, _tenant.SMTPPassword);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

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

        public string GetMailContent(HtmlTemplate module, Dictionary<string, string> keyValues)
        {
            return GetBlobMailContent(module, keyValues);
        }

        public string GetBlobMailContent(HtmlTemplate module, Dictionary<string, string> keyValues)
        {
            string blobName = _tenant.AzureBlobHtmlTemplate + module.ToString() + ".html";

            string MailText = string.Empty;

            BlobContainerClient container = new BlobContainerClient(_tenant.AzureStorageConn, _tenant.AzureStorageContainer);
            //container.Create();

            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            BlobClient blob = container.GetBlobClient(blobName);


            if (blob != null)
            {
                Stream stream = new System.IO.MemoryStream();

                stream = blob.OpenRead();

                StreamReader str = new StreamReader(stream);

                MailText = str.ReadToEnd();

                foreach (var item in keyValues)
                {
                    MailText = MailText.Replace(item.Key, item.Value);
                }
                str.Close();
            }

            return MailText;
        }

        public string MailContent(HtmlTemplate module, Dictionary<string, string> keyValues)
        {
            //string subPath = _tenant.AppRootPath + "\\wwwroot\\Template\\" + module.ToString() + ".html";

            string subPath = _tenant.HtmlTemplates + module.ToString() + ".html";

            subPath = subPath.Replace("TENANT_NAME", _tenant.SchemaName);

            string MailText = string.Empty;

            StreamReader str = new StreamReader(subPath);
            MailText = str.ReadToEnd();

            str.Close();

            foreach (var item in keyValues)
            {
                MailText = MailText.Replace(item.Key, item.Value);
            }

            return MailText;
        }

        public Result GetTicketNumber(int locationId)
        {
            return ResultWrap(new CommonRal(_tenant, false).GetTicketNumber, locationId, "GetTicketNumber");

        }

        public Result GetModelByMakeId(int makeId)
        {
            return ResultWrap(new CommonRal(_tenant, false).GetModelByMakeId, makeId, "Model");

        }

        public Result GetAllMake()
        {
            return ResultWrap(new CommonRal(_tenant, false).GetAllMake, "Make");
        }


        public Result GetUpchargeByType(UpchargeDto upchargeDto)

        {
            return ResultWrap(new CommonRal(_tenant, false).GetUpchargeByType, upchargeDto, "upcharge");

        }


        public string Template(string templateName)
        {
            //string subPath = _tenant.AppRootPath + "\\wwwroot\\Template\\" + module.ToString() + ".html";

            string subPath = _tenant.HtmlTemplates + templateName.ToString() + ".html";

            subPath = subPath.Replace("TENANT_NAME", _tenant.SchemaName);

            string MailText = string.Empty;

            StreamReader str = new StreamReader(subPath);
            MailText = str.ReadToEnd();


            str.Close();

            return MailText;
        }

        public Result GetVehiclePrint(PrintTicketDto printTicketDto)
        {
            return ResultWrap(VehicleCopyPrint, printTicketDto, "VehiclePrint");
        }

        public string VehicleCopyPrint(PrintTicketDto print)
        {
            string model = "Unk";

            if (print.Job.VehicleModel != null)
            {
                if (print.Job.VehicleModel.Contains("/"))
                {
                    model = print.Job.VehicleModel.Substring(0, print.Job.VehicleModel.IndexOf("/"));
                }
                else
                {
                    model = print.Job.VehicleModel;
                }
            }

            var body = "^XA^AJN,20^FO50,50^FD" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "^FS";
            body += "^AJN,30^FO50,90^FDIn: " + Convert.ToDateTime(print.Job.InTime).ToString("MM/dd/yyyy hh:mm tt") + "^FS^A0N,30,30^FO50,130^FDOut: " + Convert.ToDateTime(print.Job.TimeOut).ToString("hh:mm tt") + "^FS^AJN,30^FO50,170^FDClient: " + print.ClientInfo.ClientName + "^FS^AJN,20^FO50,220^FDVehicle: " + model + "^FS^AJN,20^FO50,250^GB700,3,3^FS";
            body += "^AJN,30^FO550,90^FD" + "-" + "^FS^AJN,30^FO495,170^FD" + print.ClientInfo.PhoneNumber + "^FS^AJN,20^FO440,220^FD" + print.Job.VehicleMake + "^FS^AJN,20^FO690,220^FD" + print.Job.VehicleColor + "^FS^AJN,30";
            int checkboxaxis = 280;

            if (print.JobItem != null)
            {
                foreach (var jobItem in print.JobItem)
                {
                    body += "^FO50," + checkboxaxis + "^GB20,20,1^FS^AJN,30^FO80," + checkboxaxis + "^FD" + jobItem.ServiceName.Trim() + "^FS";
                    checkboxaxis += 40;
                }
            }

            body += "^BY3,2,100^FO80," + (checkboxaxis + 80) + "^FWN^BC^FD" + print.Job.TicketNumber.ToString() + "^FS";
            body += "^AJN,30^FO80," + (checkboxaxis + 220) + "^FDTicket Number: " + print.Job.TicketNumber.ToString() + "^FS^XZ";

            return body;

        }

        public Result GetCustomerPrint(PrintTicketDto printTicketDto)
        {
            return ResultWrap(CustomerCopyPrint, printTicketDto, "CustomerPrint");
        }

        public string CustomerCopyPrint(PrintTicketDto print)
        {
            string model = "Unk";
            if (print.Job.VehicleModel != null)
            {
                if (print.Job.VehicleModel.Contains("/"))
                {
                    model = print.Job.VehicleModel.Substring(0, print.Job.VehicleModel.IndexOf("/"));
                }
                else
                {
                    model = print.Job.VehicleModel;
                }
            }

            var body = "^XA^AJN,30^FO50,50^FD" + "Client: " + print.ClientInfo.ClientName + "^FS^AJN,30^FO540,50^FD" + print.ClientInfo.PhoneNumber + "^FS";
            body += "^AJN,20^FO50,100^FD" + "Vehicle: " + model + " ^FS" +
            "^AJN,20^FO420,100^FD" + print.Job.VehicleMake + "^FS" +
            "^AJN,20^FO690,100^FD" + print.Job.VehicleColor + "^FS";

            decimal totalAmt = 0;
            int yaxis = 300;
            body += "^AJN,30^A0N,30,30^FO480,200^FD" + "Hand Car Washes" + "^FS";

            body += "^AJN,30^A0N,30,30^FO480,300^FD" + "Vehicle Upcharge" + "^FS";

            if (print.JobItem != null)
            {
                var package = print.JobItem.Where(s => s.ServiceType == "Wash Package" || s.ServiceType == "Detail Package").FirstOrDefault();

                string price = string.Empty;
                if (package != null)
                {
                    price = package.Price != 0 ? package.Price.ToString("#.00") : "0.00";
                    body += "^AJN,20^FO480,240^FD" + package.ServiceName.Trim() + " - $" + price + "^FS";

                }

                var upcharge = print.JobItem.Where(s => s.ServiceType.Contains("Upcharge")).FirstOrDefault();

                if (upcharge != null)
                {
                    yaxis += 40;
                    price = upcharge.Price != 0 ? upcharge.Price.ToString("#.00") : "0.00";
                    body += "^AJN,20^FO480," + yaxis + "^FD" + upcharge.ServiceName.Trim() + " - $" + price + "^FS";
                }
                else
                {
                    yaxis += 40;
                    body += "^AJN,20^FO480," + yaxis + "^FD" + "-" + "^FS";
                }


                var airFreshner = print.JobItem.Where(s => s.ServiceType.Contains("Air Fresheners")).FirstOrDefault();
                if (airFreshner != null)
                {
                    yaxis += 40 + 60;
                    price = airFreshner.Price != 0 ? airFreshner.Price.ToString("#.00") : "0.00";
                    body += "^AJN,20^FO480," + yaxis + "^FD" + airFreshner.ServiceName.Trim() + " - $" + price + "^FS";
                }
                else
                {
                    yaxis += 40 + 60;
                    body += "^AJN,20^FO480," + yaxis + "^FD" + "-" + "^FS";
                }

            }

            totalAmt += print.JobItem.Sum(s => s.Price);

            body += "^AJN,30^A0N,30,30^FO480," + (yaxis - 40) + "^FD" + "Air Fresheners" + "^FS";

            TimeZoneInfo tst = TimeZoneInfo.Local;

            DateTime intime = TimeZoneInfo.ConvertTime(DateTime.Parse(print.Job.InTime), TimeZoneInfo.Local, tst);
            DateTime Outtime = TimeZoneInfo.ConvertTime(DateTime.Parse(print.Job.TimeOut), TimeZoneInfo.Local, tst);
            var EstimatedTime = (Outtime - intime);

            body += "^AJN,20^FO50,600^FD" + "In: " + intime.ToString("MM/dd/yyyy hh:mm tt") + "^FS" +
            "^AJN,20^FO50,640^FD" + "Est. Out: " + Outtime.ToString("hh:mm tt") + "^FS" +
            "^AJN,20^FO50,680^FD" + "Est. Time: " + EstimatedTime.TotalMinutes + "Min^FS";

            body += @"^AJN,30
                ^A0N,30,30^FO300,720^FD" + "New Vehicle Info" + "^FS" +
                "^AJN,30^FO60,900^FD" + "Name" + "^FS" +
                "^AJN,30^FO160,920^GB600,3,3^FS" +
                "^AJN,30^FO60,940^FD" + "Phone" + "^FS^FO160,960^GB600,3,3^FS" +
                "^AJN,30^FO60,980^FD" + "Email" + "^FS" +
                "^AJN,30^FO160,1000^GB600,3,3^FS";

            if (print.Job.Barcode != null)
            {
                body += @"^AJN,20
                        ^AD^BY5,2,100
                        ^AJN,20^FO100,750^BC^FD" + print.Job.Barcode + "^FS";
            }

            body += "^AJN,20^FO50,1040^FDNote^FS";

            body += "^AJN,20^FO60,140^AD^BY4^FWB^BC,100,Y,N,N^FD" + print.Job.TicketNumber + "^FS";

            body += "^AJN,20^FO180,200^GFA,11400,11400,38," + ZebraPrint.MammothLogo + "^FS^XZ";

            return body;
        }

    }
}
