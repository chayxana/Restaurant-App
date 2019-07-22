using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Menu.API.Models;
using Xunit;

namespace Menu.API.UnitTests.Models
{
	public class FoodTest
	{
		[Theory, AutoDomainData]
		public void Test_food_auto_properties(WritablePropertyAssertion assertion)
		{
			assertion.Verify(new Properties<Food>().Select(d => d.Name));
			assertion.Verify(new Properties<Food>().Select(d => d.Description));
			assertion.Verify(new Properties<Food>().Select(d => d.Recept));
			assertion.Verify(new Properties<Food>().Select(d => d.Pictures));
			assertion.Verify(new Properties<Food>().Select(d => d.Price));
			assertion.Verify(new Properties<Food>().Select(d => d.Currency));
			assertion.Verify(new Properties<Food>().Select(d => d.Category));
			assertion.Verify(new Properties<Food>().Select(d => d.CategoryId));
			assertion.Verify(new Properties<Food>().Select(d => d.CategoryId));
		}
	}
}
