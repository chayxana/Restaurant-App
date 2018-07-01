using Albedo;
using AutoFixture.Idioms;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Models
{
	public class UserTest
	{
		[Theory, AutoDomainData]
		public void Test_user_auto_properties(WritablePropertyAssertion assertion)
		{
			assertion.Verify(new Properties<User>().Select(d => d.Orders));
			assertion.Verify(new Properties<User>().Select(d => d.UserProfile));
		}
	}
}
