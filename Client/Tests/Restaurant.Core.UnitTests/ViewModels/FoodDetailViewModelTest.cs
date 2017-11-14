using Autofac;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
	[TestFixture]
	public class FoodDetailViewModelTest : BaseAutoMockedTest<FoodDetailViewModel>
	{
		[Test, AutoDomainData]
		public void Title_should_be_selected_food_name(FoodDto foodDto)
		{
			// Given
			var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));
			
			// then
			Assert.That(viewModel.Title, Is.EqualTo(foodDto.Name));
		}

		[Test, AutoDomainData]
		public void SelectedFood_shoul_be_equal_food_wich_is_passed_from_parameter(FoodDto foodDto)
		{
			// given 
			var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));

			// then
			Assert.That(viewModel.SelectedFood, Is.EqualTo(foodDto));
		}

		[Test, AutoDomainData]
		public void Add_to_basket_should_add_order_to_Orders(FoodDto foodDto)
		{
			// Given
			var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));
			
			// when
			viewModel.AddToBasket.Execute(null);

			// then
			GetMock<IBasketViewModel>().Verify(x => x.AddOrder(viewModel.CurrentOrder), Times.Once);
		}

		[Test, AutoDomainData]
		public void GoToBasket_should_navigate_to_Basket_page(FoodDto foodDto)
		{
			// Given
			var viewModel = Mocker.Create<FoodDetailViewModel>(new NamedParameter("selectedFood", foodDto));

			// when
			viewModel.GoToBasket.Execute(null);

			// then
			GetMock<INavigationService>().Verify(x => x.NavigateAsync(viewModel.BasketViewModel));
		}
	}
}
