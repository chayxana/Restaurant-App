using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Common.DataTransferObjects;
using Restaurant.ViewModels;

namespace Restaurant.UnitTests.ViewModels
{
    [TestFixture]
    public class SignInViewModelTest
    {
        private Mock<IAuthenticationManager> _authenticationManager;
        private Mock<IAutoMapperFacade> _autoMapperFacade;
        private SignInViewModel _signInViewModel;

        [OneTimeSetUp]
        public void Init()
        {
            _authenticationManager = new Mock<IAuthenticationManager>();
            _autoMapperFacade = new Mock<IAutoMapperFacade>();
            _signInViewModel = new SignInViewModel(_authenticationManager.Object, _autoMapperFacade.Object, null);
        }

        [Test]
        public void Login_with_valid_data_should_be_ok()
        {
            _signInViewModel.Email = "12@123.com";
            _signInViewModel.Password = "test123";


            var loginDto = new LoginDto { Login = _signInViewModel.Email, Password = _signInViewModel.Password };

            _autoMapperFacade.Setup(x => x.Map<LoginDto>(It.IsAny<SignInViewModel>()))
                .Returns(loginDto);

            //_authenticationManager.Setup(x => x.Login(It.IsAny<LoginDto>())).Returns(Task.FromResult(new AuthenticationResult
            //{
            //    ok = true
            //}));

            _signInViewModel.Login.Execute(null);
        }
    }
}
