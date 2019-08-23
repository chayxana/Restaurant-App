using Autofac;
using NUnit.Framework;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.UnitTests.ViewModels
{
    [TestFixture]
    public class FoodDetailViewModelTest : BaseAutoMockedTest<FoodDetailViewModel>
    {
        [Test]
        [AutoDomainData]
        public void Add_to_basket_should_add_order_to_Orders(FoodDto foodDto)
        {
            // Given
            var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));

            // when
            viewModel.AddToBasket.Execute(null);

            // then
        }

        [Test]
        [AutoDomainData]
        public void GoToBasket_should_navigate_to_Basket_page(FoodDto foodDto)
        {
            // Given
            var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));

            // when
            viewModel.GoToBasket.Execute(null);

            // then
            GetMock<INavigationService>().Verify(x => x.NavigateAsync(typeof(IBasketViewModel)));
        }

        [Test]
        [AutoDomainData]
        public void SelectedFood_should_be_equal_food_which_is_passed_from_parameter(FoodDto foodDto)
        {
            // given 
            var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));

            // then
            Assert.That(viewModel.SelectedFood, Is.EqualTo(foodDto));
        }

        [Test]
        [AutoDomainData]
        public void Title_should_be_selected_food_name(FoodDto foodDto)
        {
            // Given
            var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));

            // then
            Assert.That(viewModel.Title, Is.EqualTo(foodDto.Name));
        }
    }
}