using System.Reflection;
using System.Threading.Tasks;
using Albedo;
using AutoFixture;
using AutoFixture.Idioms;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;

namespace Restaurant.Core.UnitTests.ViewModels.Android
{
    public class MasterViewModelTests : BaseAutoMockedTest<MasterViewModel>
    {
		[Test, AutoDomainData]
	    public async Task Given_access_token_GetUserInfo_should_return_UserViewModel(UserDto userDto, string accessToken)
	    {
			// given
		    var masterViewModel = ClassUnderTest;
			var userViewModel = new UserViewModel();
			GetMock<IAuthenticationProvider>().Setup(x => x.GetAccessToken()).Returns(Task.FromResult(accessToken));
		    GetMock<IMapper>().Setup(x => x.Map<UserViewModel>(userDto)).Returns(userViewModel);
		    GetMock<IAccountApi>().Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(Task.FromResult(userDto));
			// when
		    await masterViewModel.LoadUserInfo();

			Assert.That(masterViewModel.UserViewModel, Is.EqualTo(userViewModel));
	    }

		[Test, AutoDomainData]
	    public void Test_MasterViewModel_auto_properties()
		{
			var masterViewModel = ClassUnderTest;

			masterViewModel.UserViewModel = new UserViewModel();
			Assert.That(masterViewModel.UserViewModel, Is.Not.Null);

			masterViewModel.SelectedNavigationItem = NavigationItem.Foods;
			Assert.That(masterViewModel.SelectedNavigationItem, Is.EqualTo(NavigationItem.Foods));

			Assert.That(masterViewModel.Title, Is.EqualTo("Menu"));
		}
	}
}
