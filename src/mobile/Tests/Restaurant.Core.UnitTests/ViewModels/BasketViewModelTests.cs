using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class BasketViewModelTests : BaseAutoMockedTest<BasketViewModel>
    {
        [Test]
        [AutoDomainData]
        public void Given_basket_item_list_and_quantity_of_items_should_be_sum_if_order_food_is_same_as_previus(
            FoodViewModel food)
        {
            // given
            var publisher = new Subject<IBasketItemViewModel>();


            var viewModel = ClassUnderTest;
            var firstBasketItem = new BasketItemViewModel(food)
            {
                Quantity = 2
            };
            var secondBasketItem = new BasketItemViewModel(food)
            {
                Quantity = 3
            };

            // when
            publisher.OnNext(firstBasketItem);
            publisher.OnNext(secondBasketItem);

            // then
            Assert.That(firstBasketItem.TotalPrice, Is.EqualTo(5 * food.Price));
            Assert.That(viewModel.Items.Count, Is.EqualTo(1));

            Assert.That(viewModel.Items, Is.Not.Null);
            // ReSharper disable once PossibleNullReferenceException
            Assert.That(viewModel.Items.FirstOrDefault().Quantity, Is.EqualTo(5));
        }


        [Test]
        public void Title_should_be_your_basket()
        {
            var publisher = new Subject<IBasketItemViewModel>();


            Assert.That(ClassUnderTest.Title, Is.EqualTo("Your basket"));
        }

        [Test]
        [AutoDomainData]
        public void Complete_order_should_push_OrderDto_to_server(IEnumerable<OrderItemDto> orders)
        {
            var publisher = new Subject<IBasketItemViewModel>();

            GetMock<IMapper>()
                .Setup(x => x.Map<IEnumerable<OrderItemDto>>(It.IsAny<ObservableCollection<IBasketItemViewModel>>()))
                .Returns(orders);

            GetMock<IOrdersApi>().Setup(x => x.Create(It.IsAny<OrderDto>())).Returns(Task.CompletedTask);

            ClassUnderTest.CompleteOrder.Execute(null);

            GetMock<INavigationService>().Verify(x => x.NavigateToRoot(), Times.Once);
        }

        [Test]
        [AutoDomainData]
        public void Add_orders_should_change_orders_count_as_string(FoodViewModel food)
        {
            var basketItemViewModel = new BasketItemViewModel(food);
            var publisher = new Subject<IBasketItemViewModel>();


            var viewModel = ClassUnderTest;
            publisher.OnNext(basketItemViewModel);
        }
    }
}