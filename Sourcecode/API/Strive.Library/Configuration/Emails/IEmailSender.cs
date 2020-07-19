using System.Threading.Tasks;

namespace Strive.Library.Configuration.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}