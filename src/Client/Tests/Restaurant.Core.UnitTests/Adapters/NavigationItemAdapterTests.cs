using NUnit.Framework;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.Factories;
using Restaurant.Core.Adapters;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.Adapters
{
    public class NavigationItemAdapterTests : BaseAutoMockedTest<NavigationItemAdapter>
    {
		[Test]
	    public void Given_Navigation_item_GetViewModelFromNavigationItem_should_return_INavigatableViewModel()
		{
			var navigationItemAdapter = ClassUnderTest;
			
			var viewModelFactory = GetMock<IViewModelFactory>();
			viewModelFactory.Setup(x => x.GetViewModel(typeof(FoodsViewModel))).Returns(new FoodsViewModel());
			viewModelFactory.Setup(x => x.GetViewModel(typeof(OrdersViewModel))).Returns(new OrdersViewModel());

			var foodsViewModel = navigationItemAdapter.GetViewModelFromNavigationItem(NavigationItem.Foods);
			Assert.That(foodsViewModel, Is.InstanceOf<FoodsViewModel>());

			var ordersViewModel = navigationItemAdapter.GetViewModelFromNavigationItem(NavigationItem.Orders);
			Assert.That(ordersViewModel, Is.InstanceOf<OrdersViewModel>());

			var @default = navigationItemAdapter.GetViewModelFromNavigationItem((NavigationItem) 2000);
			Assert.That(@default, Is.Null);
		}
	}
}
