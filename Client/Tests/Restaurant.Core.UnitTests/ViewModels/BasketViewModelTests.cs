using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class BasketViewModelTests : BaseAutoMockedTest<BasketViewModel>
    {
        [Test, AutoDomainData]
        public void Given_order_should_add_orders_list_and_quantity_of_order_should_be_sum_if_order_food_is_same_as_previus(FoodDto food)
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
            Assert.That(viewModel.Orders.Count, Is.EqualTo(1));
            Assert.That(viewModel.Orders.FirstOrDefault().Quantity, Is.EqualTo(5));
        }

        [Test, AutoDomainData]
        public void Given_order_should_clone_order_object_before_adding_to_orders_list(FoodDto food, FoodDto food2)
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
    }
}
