using System.Threading.Tasks;

namespace Shared
{
    public class SendUserEmail : ISendUserEmail
    {
        private readonly IEmailService _emailService;
        private readonly IUser _user;
        public SendUserEmail(IEmailService emailService, IUser user)
        {
            _emailService = emailService;
            _user = user;
        }

        public Task<EmailOutput> SendWelcomeEmail(string message)
        {
            return _emailService.SendEmail(_user.Email, "Welcome to Sopro!", message);
        }
    }
}
