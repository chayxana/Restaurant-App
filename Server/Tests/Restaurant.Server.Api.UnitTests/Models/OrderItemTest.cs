using Albedo;
using AutoFixture.Idioms;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Models
{
    public class OrderItemTest
    {
	    [Theory, AutoDomainData]
	    public void Test_order_item_auto_properties(WritablePropertyAssertion assertion)
	    {
		    assertion.Verify(new Properties<OrderItem>().Select(d => d.Quantity));
		    assertion.Verify(new Properties<OrderItem>().Select(d => d.FoodId));
		    assertion.Verify(new Properties<OrderItem>().Select(d => d.OderId));
		}
	}
}
