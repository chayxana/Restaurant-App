using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    [TestFixture]
    public class WelcomeViewModelTest : BaseAutoMockedTest<WelcomeViewModel>
    {
        [Test]
        public void GoLogin_should_navigate_to_SignIn_page()
        {
            // when
            ClassUnderTest.GoLogin.Execute(null);

            // Then
            GetMock<INavigationService>().Verify(x => x.NavigateAsync(typeof(ISignInViewModel)), Times.Once);
        }

        [Test]
        public void GoRegister_should_navigate_to_SignUp_page()
        {
            // when
            ClassUnderTest.GoRegister.Execute(null);

            // Then
            GetMock<INavigationService>().Verify(x => x.NavigateAsync(typeof(ISignUpViewModel)), Times.Once);
        }

        [Test]
        public void Title_should_be_Welcome_page()
        {
            Assert.That(ClassUnderTest.Title, Is.EqualTo("Welcome page"));
        }
    }
}