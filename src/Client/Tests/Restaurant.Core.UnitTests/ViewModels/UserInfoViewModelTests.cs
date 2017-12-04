using Albedo;
using AutoFixture.Idioms;
using NUnit.Framework;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class UserInfoViewModelTests
    {
		[Test, AutoDomainData]
	    public void Test_UserInfoViewModel_auto_properties(WritablePropertyAssertion assertion)
		{
			var properties = new Properties<UserInfoViewModel>();
			assertion.Verify(properties.Select(x => x.BirthDate));
			assertion.Verify(properties.Select(x => x.LastName));
			assertion.Verify(properties.Select(x => x.Name));
			assertion.Verify(properties.Select(x => x.Picture));
		}
    }
}
