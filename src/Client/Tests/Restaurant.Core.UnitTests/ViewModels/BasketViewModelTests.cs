using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ReactiveUI;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class BasketViewModelTests : BaseAutoMockedTest<BasketViewModel>
    {
        [Test, AutoDomainData]
        public void Given_order_should_add_orders_list_and_quantity_of_order_should_be_sum_if_order_food_is_same_as_previus(FoodViewModel food)
        {
            // given
            var viewModel = ClassUnderTest;
            var orderViewModel = new OrderViewModel(food)
            {
                Quantity = 2
            };
            var orderViewModel2 = new OrderViewModel(food.Clone())
            {
                Quantity = 3
            };

            // when
            viewModel.AddOrder(orderViewModel);
            viewModel.AddOrder(orderViewModel2);

            // then
            Assert.That(orderViewModel.TotalPrice, Is.EqualTo(2 * food.Price));
            Assert.That(viewModel.Orders.Count, Is.EqualTo(1));
            Assert.That(viewModel.Orders.FirstOrDefault().Quantity, Is.EqualTo(5));
            
        }

        [Test, AutoDomainData]
        public void Given_order_should_clone_order_object_before_adding_to_orders_list(FoodViewModel food, FoodViewModel food2)
        {
            // given
            var viewModel = ClassUnderTest;
            var orderViewModel = new OrderViewModel(food)
            {
                Quantity = 2
            };

            var orderViewModel2 = new OrderViewModel(food2)
            {
                Quantity = 1
            };

            // when
            viewModel.AddOrder(orderViewModel);

            // this will be test Order Clone 
            viewModel.AddOrder(orderViewModel);

            // then
            Assert.That(viewModel.Orders.Count, Is.EqualTo(1));
            Assert.That(viewModel.Orders.FirstOrDefault().Quantity, Is.EqualTo(4));


            // when
            viewModel.AddOrder(orderViewModel2);
            Assert.That(viewModel.Orders.Count, Is.EqualTo(2));

            Assert.That(viewModel.Orders[0].Food, Is.Not.EqualTo(viewModel.Orders[1].Food));
        }

        [Test]
        public void Title_should_be_your_basket()
        {
            Assert.That(ClassUnderTest.Title, Is.EqualTo("Your basket"));
        }

        [Test, AutoDomainData]
        public void Complete_order_should_push_OrderDto_to_server(IEnumerable<OrderItemDto> orders)
        {
            GetMock<IAutoMapperFacade>()
                .Setup(x => x.Map<IEnumerable<OrderItemDto>>(It.IsAny<ReactiveList<IOrderViewModel>>()))
                .Returns(orders);

            GetMock<IOrdersApi>().Setup(x => x.Create(It.IsAny<OrderDto>())).Returns(Task.CompletedTask);

            ClassUnderTest.CompleteOrder.Execute(null);

            GetMock<INavigationService>().Verify(x => x.NavigateToRoot(), Times.Once);
        }

        [Test, AutoDomainData]
        public void Add_orders_should_change_orders_count_as_string(OrderViewModel orderViewModel)
        {
            var viewModel = ClassUnderTest;
            viewModel.Orders.Add(orderViewModel);

            Assert.That(viewModel.OrdersCount, Is.EqualTo("1"));
        }
    }
}
