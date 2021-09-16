using System.Threading.Tasks;
using NSubstitute;
using Shared;
using Shouldly;
using Xunit;

namespace Testing
{
    public class UserEmailSenderShould : TestBase
    {
        private readonly ISendUserEmail _userEmailService;
        private readonly IUser _user;
        private readonly IUserPreferences _userPreferences;

        public UserEmailSenderShould()
        {
            _userEmailService = Resolve<ISendUserEmail>();
            _user = Resolve<IUser>();
            _userPreferences = Resolve<IUserPreferences>();
        }

        [Fact]
        public async Task SendGMailEmail()
        {
            // Arrange
            _userPreferences.EmailClient.Returns(EmailProviders.GMail);
            _user.Email.Returns("demo@demo.com");

            // Act
            var emailOutput = await _userEmailService.SendWelcomeEmail("hi");

            // Assert
            emailOutput.EmailProvider.ShouldBe(EmailProviders.GMail);
        }

        [Fact]
        public async Task SendDefaultSendGridEmail()
        {
            // Arrange
            _userPreferences.EmailClient.Returns((string)null);
            _user.Email.Returns("demo@demo.com");

            // Act
            var emailOutput = await _userEmailService.SendWelcomeEmail("hi");

            // Assert
            emailOutput.EmailProvider.ShouldBe(EmailProviders.SendGrid);
        }
    }
}