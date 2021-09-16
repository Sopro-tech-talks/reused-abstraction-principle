using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NSubstitute;
using Shared;

namespace Testing
{
    public abstract class TestBase
    {
        private IWindsorContainer IocContainer { get; set; }
        protected TestBase()
        {
            IocContainer = new WindsorContainer();
            InitializeInjection();
        }

        protected T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        private void InitializeInjection()
        {
            IocContainer.Register(Component.For<IGMailEmailService>().ImplementedBy<GMailService>());
            IocContainer.Register(Component.For<ISendGridEmailService>().ImplementedBy<SendGridService>());
            IocContainer.Register(Component.For<IAzureEmailService>().ImplementedBy<AzureEmailService>());
            IocContainer.Register(Component.For<IUserPreferences>().UsingFactoryMethod(() => Substitute.For<IUserPreferences>()).LifestyleSingleton());
            IocContainer.Register(Component.For<IUser>().UsingFactoryMethod(() => Substitute.For<IUser>()).LifestyleSingleton());

            IocContainer.AddFacility<TypedFactoryFacility>();

            IocContainer.Register(Component.For<Func<IGMailEmailService>>().AsFactory());
            IocContainer.Register(Component.For<Func<ISendGridEmailService>>().AsFactory());
            IocContainer.Register(Component.For<Func<IAzureEmailService>>().AsFactory());

            IocContainer.Register(Component.For<IEmailService>().ImplementedBy<EmailSenderService>());
            IocContainer.Register(Component.For<ISendUserEmail>().ImplementedBy<SendUserEmail>());
        }
    }
}
