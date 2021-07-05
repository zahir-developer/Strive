using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.Security.Authentication;
using MailKit.Security;
using System.Text;
using System.Threading;
using Strive.BusinessLogic.EmailHelper.Dto;

namespace Strive.BusinessLogic.EmailHelper
{

  
    public class EmailSender
    {
        private EmailSettingDto _settings;
        //private readonly IAuthManager _authManager;


        public EmailSender(EmailSettingDto EmailSettingDto)
        {
            _settings = EmailSettingDto;
            //_authManager = authManager;
        }

        public void SendEmail(MessageDto message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public void Initialize(string subject, string emailIds , string content)
        {
            MessageDto msg = new MessageDto();

            var emailToList = new List<InternetAddress>();

            MailboxAddress address;
            foreach(string emailId in emailIds.Split(','))
            {
                address = new MailboxAddress(emailId, emailId);
                emailToList.Add(address);
            }

            msg.To = emailToList;
            msg.Content = content;
            msg.Subject = subject;
            SendEmail(msg);
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.FromEmail,_settings.FromEmail));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        public void Send(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.SslProtocols |= SslProtocols.Tls;
                    client.CheckCertificateRevocation = false;
                    client.Connect(_settings.PrimaryDomain, _settings.PrimaryPort, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_settings.UsernameEmail, _settings.UsernamePassword);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public async Task SendEmailAsync(string toEmail, string toName, string subject, string message, string fromMail, string fromName, string subheading = "", string fromRole = "", bool welcome = false, string ccEmail = "", string HashedResetPassword = null, string host = null)
        {
            var telemetry = new TelemetryClient();
            //try
            //{

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(fromMail, "QwikTime")
            };
            mail.To.Add(new MailAddress(toEmail));
            if (ccEmail != "")
            {
                mail.CC.Add(new MailAddress(ccEmail));
            }
            mail.Subject = "QwikTime Timesheets - " + subject;
            if (welcome)
            {
                //ResetAuthInputRequest resetAuthInputRequest = new ResetAuthInputRequest();
                //resetAuthInputRequest.EmailId = toEmail;
                //resetAuthInputRequest.ActionType = "Reset";
                //resetAuthInputRequest.ResetAction = "Reset";

                //var resetResponse = _authManager.GenerateResetPassword(resetAuthInputRequest);
                mail.Body = @"<html>
                    <head>
                        <style type='text/css'>
                            h1.rubymail{color: black;font-size:20px;}
                            h2.rubymail{color: black;font-size:18px;}
                            div.rubymail{font-size:16px;line-height:22px;font-family:helvetica, Arial, sans-serif;
                                color:#777777;text-align:left;background-color:whitesmoke;width:700px;
                                border:1px solid gray;padding:30px;}</style>
                    </head>
                    <body>
                        <div class='rubymail'>
                                <h1 class='rubymail'><center>" + subject + @"</center></h1>" + (string.IsNullOrEmpty(subheading) ? "" :
                         "<h2 class='rubymail'><center>" + subheading + "</center></h2>") +
                         "<p class='rubymail'><font color='0F31AA'> " + "Hi" + "  " + toName + @"</font><br/></p>
                                  <p>" + message + @"</p><br/> <Link to='/http://" + host + "/user/resetpassword/'> " + "http://" + host + "/user/resetpassword/" + HashedResetPassword + @" </Link><p>"

                         + ((welcome) ? "Regards" + @"<br/>Team Telliant" : "Thanks" + @"<font color='0F31AA'>" + fromName + @"<br/>" +
                         (string.IsNullOrEmpty(fromRole) ? "" : fromRole + ", ") + @"QwikTime Timesheets</font>") + @"
                                </p></div>
                    </body>  
</html>";

            }
            else
            {
                mail.Body = @"<html>
                    <head>
                        <style type='text/css'>
                            h1.rubymail{color: black;font-size:20px;}
                            h2.rubymail{color: black;font-size:18px;}
                            div.rubymail{font-size:16px;line-height:22px;font-family:helvetica, Arial, sans-serif;
                                color:#777777;text-align:left;background-color:whitesmoke;width:700px;
                                border:1px solid gray;padding:30px;}</style>
                    </head>
                    <body>
                        <div class='rubymail'>
                                <h1 class='rubymail'><center>" + subject + @"</center></h1>" + (string.IsNullOrEmpty(subheading) ? "" :
                          "<h2 class='rubymail'><center>" + subheading + "</center></h2>") +
                          "<p class='rubymail'><font color='0F31AA'> " + ((welcome) ? "Dear" + "  " : "Hi" + "  ") + toName + @"</font><br/></p>
                                <p>" + message + @"</p><p>"
                          + ((welcome) ? "Regards" + @"<br/>Team Telliant" : "Thanks" + @"<font color='0F31AA'>" + fromName + @"<br/>" +
                          (string.IsNullOrEmpty(fromRole) ? "" : fromRole + ", ") + @"QwikTime Timesheets</font>") + @"
                                </p></div>
                    </body>  
</html>";
            }
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(_settings.PrimaryDomain, _settings.PrimaryPort))
            {
                smtp.Credentials = new NetworkCredential(_settings.UsernameEmail, _settings.UsernamePassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
            telemetry.TrackTrace("Email sent successfully", SeverityLevel.Information,
                new Dictionary<string, string> { { "emailAddress", toEmail }, { "subject", subject } });
            //}
            //catch (Exception ex)
            //{
            //    telemetry.TrackTrace("Email send error",
            //                    SeverityLevel.Error,
            //                    new Dictionary<string, string> { { "emailAddress", toEmail },
            //                        { "ExceptionDetail", ex.ToString()} });
            //}
        }
    }
}
