using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Models
{
	public class OrderTest
	{
		[Theory, AutoDomainData]
		public void Test_order_auto_properties(WritablePropertyAssertion assertion)
		{
			assertion.Verify(new Properties<Order>().Select(d => d.DateTime));
			assertion.Verify(new Properties<Order>().Select(d => d.OrderItems));
			assertion.Verify(new Properties<Order>().Select(d => d.UserId));
			assertion.Verify(new Properties<Order>().Select(d => d.Id));
		}
	}
}
