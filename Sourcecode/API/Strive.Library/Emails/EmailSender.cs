using Strive.Library.Configuration.Emails;
using System.Threading.Tasks;
//using SampleProject.Application.Configuration.Emails;

namespace Strive.Library.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            // Integration with email service.

            return;
        }
    }
}