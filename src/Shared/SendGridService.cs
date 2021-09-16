using System.Threading.Tasks;

namespace Shared
{
    public class SendGridService : ISendGridEmailService
    {
        public Task<EmailOutput> SendEmail(string to, string subject, string message) =>
            Task.FromResult(new EmailOutput
            {
                Content = $"Sent email through SendGrid to {to} with subject: {subject} and content: {message}",
                EmailProvider = EmailProviders.SendGrid
            });
    }
}