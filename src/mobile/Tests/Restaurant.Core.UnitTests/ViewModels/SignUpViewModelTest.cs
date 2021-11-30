using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class SignUpViewModelTest : BaseAutoMockedTest<SignUpViewModel>
    {
        [Test, AutoDomainData]
		[Ignore("Removed")]
        public void Given_valid_parameters_Register_should_register_and_login(RegisterDto registerDto)
        {
            // Given
            var viewModel = ClassUnderTest;
            viewModel.Email = "emailTest1@Test.com";
            viewModel.Password = "123";
            viewModel.ConfirmPassword = "123";
            var tokenResponse = new TokenResponse()
            {
                AccessToken = "tsatdatsfa",
                IsError = false,
                HttpStatusCode = HttpStatusCode.OK
            };

            var authenticationManager = GetMock<IAuthenticationProvider>();

            authenticationManager.Setup(x => x.Register(registerDto))
                .Returns(Task.FromResult<HttpResponseMessage>(new HttpResponseMessage()));
            authenticationManager.Setup(x => x.Login(It.IsAny<LoginDto>()))
                .Returns(Task.FromResult(tokenResponse));

            GetMock<IMapper>().Setup(x => x.Map<RegisterDto>(It.IsAny<SignUpViewModel>())).Returns(registerDto);


            // when
            ClassUnderTest.Register.Execute(null);

            // then
            GetMock<INavigationService>().Verify(x => x.NavigateAsync(typeof(IMainViewModel)), Times.Once);
        }

        [Test]
        public void Title_should_be_sign_up()
        {
            Assert.That(ClassUnderTest.Title, Is.EqualTo("Sign Up"));
        }
    }
}
