using System.Threading.Tasks;

namespace Shared
{
    public interface IEmailService
    {
        Task<EmailOutput> SendEmail(string to, string subject, string message);
    }
}