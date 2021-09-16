using System.Threading.Tasks;

namespace Shared
{
    public class GMailService : IGMailEmailService
    {
        public Task<EmailOutput> SendEmail(string to, string subject, string message) =>
            Task.FromResult(new EmailOutput
            {
                Content = $"Sent email through GMail to {to} with subject: {subject} and content: {message}",
                EmailProvider = EmailProviders.GMail
            });
    }
}