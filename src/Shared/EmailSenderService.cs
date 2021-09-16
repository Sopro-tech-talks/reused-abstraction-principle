using System;
using System.Threading.Tasks;

namespace Shared
{
    public class EmailSenderService : IEmailService
    {
        private readonly Func<IGMailEmailService> _gmailServiceFunc;
        private readonly Func<ISendGridEmailService> _sendGridEmailFunc;
        private readonly Func<IAzureEmailService> _azureEmailFunc;
        private readonly IUserPreferences _userPreferences;
        private Func<IEmailService> _emailServiceFunc;

        public EmailSenderService(Func<IGMailEmailService> gmailServiceFunc, Func<ISendGridEmailService> sendGridEmailFunc, Func<IAzureEmailService> azureEmailFunc, IUserPreferences userPreferences)
        {
            _gmailServiceFunc = gmailServiceFunc;
            _sendGridEmailFunc = sendGridEmailFunc;
            _azureEmailFunc = azureEmailFunc;
            _userPreferences = userPreferences;
        }

        public Task<EmailOutput> SendEmail(string to, string subject, string message)
        {
            _emailServiceFunc = _userPreferences.EmailClient switch
            {
                EmailProviders.GMail => _gmailServiceFunc,
                EmailProviders.Azure => _azureEmailFunc,
                EmailProviders.SendGrid => _sendGridEmailFunc,
                _ => _sendGridEmailFunc
            };

            return _emailServiceFunc().SendEmail(to, subject, message);
        }
    }
}