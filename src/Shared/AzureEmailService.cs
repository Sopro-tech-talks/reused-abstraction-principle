using System.Threading.Tasks;

namespace Shared
{
    public class AzureEmailService : IAzureEmailService
    {
        public Task<EmailOutput> SendEmail(string to, string subject, string message) =>
            Task.FromResult(new EmailOutput
            {
                Content = $"Sent email through Azure to {to} with subject: {subject} and content: {message}",
                EmailProvider = EmailProviders.Azure
            });
    }
}