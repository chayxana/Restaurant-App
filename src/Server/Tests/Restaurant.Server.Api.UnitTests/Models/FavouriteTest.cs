using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Models
{
    public class FavouriteTest
    {
	    [Theory, AutoDomainData]
	    public void Test_Favourite_auto_properties(WritablePropertyAssertion assertion)
	    {
		    assertion.Verify(new Properties<Favorite>().Select(c => c.FoodId));
		    assertion.Verify(new Properties<Favorite>().Select(c => c.UserId));
		}

	    [Theory, AutoDomainData]
	    public void Test_Favourite_Food_auto_properties(WritablePropertyAssertion assertion)
	    {
		    assertion.Verify(new Properties<FavoriteFood>().Select(c => c.FoodId));
		    assertion.Verify(new Properties<FavoriteFood>().Select(c => c.UserId));
	    }
	}
}
