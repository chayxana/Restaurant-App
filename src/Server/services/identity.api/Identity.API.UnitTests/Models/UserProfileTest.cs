using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Identity.API.Model.Entities;
using Xunit;

namespace Identity.API.UnitTests.Models
{
    public class UserProfileTest
    {
	    [Theory, AutoDomainData]
	    public void Test_user_profile_auto_properties(WritablePropertyAssertion assertion)
	    {
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.ApplicationUser));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.BirthDate));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.LastName));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.Name));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.Picture));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.UserId));
		}
	}
}
