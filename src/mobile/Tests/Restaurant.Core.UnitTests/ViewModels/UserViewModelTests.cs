using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using NUnit.Framework;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class UserViewModelTests : BaseAutoMockedTest<UserViewModel>
    {
		[Test, AutoDomainData]
	    public void Test_UserViewModel_auto_properties()
		{
			var userViewModel = ClassUnderTest;
			userViewModel.Email = "aaa@aaa";
			userViewModel.UserInfoViewModel = new UserInfoViewModel();

			Assert.That(userViewModel.Email, Is.EqualTo("aaa@aaa"));
			Assert.That(userViewModel.UserInfoViewModel, Is.Not.Null);
		}
    }
}
