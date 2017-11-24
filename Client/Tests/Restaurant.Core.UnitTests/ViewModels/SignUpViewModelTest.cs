using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class SignUpViewModelTest : BaseAutoMockedTest<SignUpViewModel>
    {
        [Test, AutoDomainData]
        public void Given_valid_parameters_Register_should_register_and_login(RegisterDto registerDto)
        {
            // Given
            var viewModel = ClassUnderTest;
            viewModel.Name = "Test1";
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
                .Returns(Task.FromResult<object>(new object()));
            authenticationManager.Setup(x => x.Login(It.IsAny<LoginDto>()))
                .Returns(Task.FromResult(tokenResponse));

            GetMock<IAutoMapperFacade>().Setup(x => x.Map<RegisterDto>(It.IsAny<SignUpViewModel>())).Returns(registerDto);


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
