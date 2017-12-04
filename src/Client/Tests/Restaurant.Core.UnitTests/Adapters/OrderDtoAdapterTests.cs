using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.Adapters;

namespace Restaurant.Core.UnitTests.Adapters
{
	public class OrderDtoAdapterTests : BaseAutoMockedTest<OrderDtoAdapter>
	{
		[Test, AutoDomainData]
		public void Given_order_view_models_should_return_order_dto(ReactiveList<IOrderViewModel> orderViewModels, IEnumerable<OrderItemDto> orderItems)
		{
			// given
			GetMock<IAutoMapperFacade>().Setup(x => x.Map<IEnumerable<OrderItemDto>>(orderViewModels)).Returns(orderItems);
			GetMock<IDateTimeFacade>().SetupGet(x => x.Now).Returns(DateTime.Today);
			
			// when
			var result = ClassUnderTest.GetOrderFromOrderViewModels(orderViewModels);

			// then
			Assert.That(result, Is.Not.Null);
			Assert.That(result.DateTime, Is.EqualTo(DateTime.Today));
			Assert.That(result.OrderItems, Is.EqualTo(orderItems));
		}
	}
}