using System;
using System.Threading.Tasks;
using NSubstitute;
using Shared;
using Shouldly;
using Xunit;

namespace Testing
{
    public class EmailServiceShould : TestBase
    {
        private readonly Func<IGMailEmailService> _gmailServiceFunc;
        private readonly Func<ISendGridEmailService> _sendGridEmailFunc;
        private readonly Func<IAzureEmailService> _azureEmailFunc;
        private readonly IUserPreferences _userPreferences;
        private readonly IEmailService _emailService;

        public EmailServiceShould()
        {
            _userPreferences = Resolve<IUserPreferences>();
            _sendGridEmailFunc = Resolve<Func<ISendGridEmailService>>();
            _azureEmailFunc = Resolve<Func<IAzureEmailService>>();
            _gmailServiceFunc = Resolve<Func<IGMailEmailService>>();
            _emailService = Resolve<IEmailService>();
        }

        [Theory]
        [InlineData(EmailProviders.Azure, EmailProviders.Azure)]
        [InlineData(EmailProviders.GMail, EmailProviders.GMail)]
        [InlineData(EmailProviders.SendGrid, EmailProviders.SendGrid)]
        [InlineData("", EmailProviders.SendGrid)]
        public async Task ResolveProperEmailProvider(string emailPreferenceProvider, string expectedEmailProvider)
        {
            // Arrange
            _userPreferences.EmailClient.Returns(emailPreferenceProvider);

            // Act
            var emailOutput = await _emailService.SendEmail("demo@demo.com", "test subject", "some message");

            // Assert
            _emailService.ShouldBeAssignableTo<IEmailService>();
            emailOutput.ShouldNotBeNull();
            emailOutput.EmailProvider.ShouldBe(expectedEmailProvider);
        }
    }
}