using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.ViewModels
{
    public class FoodDetailViewModel : BaseViewModel, IFoodDetailViewModel
    {
        private IBasketViewModel _basketViewModel;
        private IOrderViewModel _currentOrderViewModel;

        public FoodDetailViewModel(
            FoodDto selectedFood,
            IBasketViewModel basketViewModel,
            INavigationService navigationService)
        {
            SelectedFood = selectedFood;
            BasketViewModel = basketViewModel;
            CurrentOrder = new OrderViewModel(SelectedFood);

            AddToBasket = ReactiveCommand.Create(() =>
                basketViewModel.AddOrder(CurrentOrder));

            GoToBasket = ReactiveCommand.CreateFromTask(async () =>
                await navigationService.NavigateAsync(basketViewModel));
        }

        public override string Title => SelectedFood.Name;

        public FoodDto SelectedFood { get; }

        public IBasketViewModel BasketViewModel
        {
            get => _basketViewModel;
            set => this.RaiseAndSetIfChanged(ref _basketViewModel, value);
        }

        public IOrderViewModel CurrentOrder
        {
            get => _currentOrderViewModel;
            private set => this.RaiseAndSetIfChanged(ref _currentOrderViewModel, value);
        }

        /// <summary>
        ///     Adds current order to BasketViewModel Orders list
        /// </summary>
        public ICommand AddToBasket { [UsedImplicitly] get; }

        /// <summary>
        ///     Navigates to Basket ViewModel
        /// </summary>
        public ICommand GoToBasket { [UsedImplicitly] get; }
    }
}