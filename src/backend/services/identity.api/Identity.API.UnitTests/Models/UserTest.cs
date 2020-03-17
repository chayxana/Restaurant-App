using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Identity.API.Model.Entities;
using Xunit;

namespace Identity.API.UnitTests.Models
{
	public class UserTest
	{
		[Theory, AutoDomainData]
		public void Test_user_auto_properties(WritablePropertyAssertion assertion)
		{
			assertion.Verify(new Properties<ApplicationUser>().Select(d => d.UserProfile));
		}
	}
}
