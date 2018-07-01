using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Models
{
    public class UserProfileTest
    {
	    [Theory, AutoDomainData]
	    public void Test_user_profile_auto_properties(WritablePropertyAssertion assertion)
	    {
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.User));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.BirthDate));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.LastName));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.Name));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.Picture));
		    assertion.Verify(new Properties<UserProfile>().Select(d => d.UserId));
		}
	}
}
