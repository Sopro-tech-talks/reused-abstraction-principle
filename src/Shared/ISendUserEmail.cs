using System.Threading.Tasks;

namespace Shared
{
    public interface ISendUserEmail
    {
        Task<EmailOutput> SendWelcomeEmail(string message);
    }
}