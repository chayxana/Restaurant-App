using System;
using System.Reactive.Linq;
using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels.Food
{
    public class FoodDetailViewModel : BaseViewModel, IFoodDetailViewModel
    {
        private string _basketItemsCount;
        private IBasketItemViewModel _currentBasketItemViewModel;

        public FoodDetailViewModel(
            IFoodViewModel selectedFood,
            INavigationService navigationService,
            IBasketItemsService basketItemsService)
        {
            SelectedFood = selectedFood;

            CurrentBasketItem = new BasketItemViewModel(SelectedFood);

            AddToBasket = ReactiveCommand.Create(() =>
                basketItemsService.Add(CurrentBasketItem));

            GoToBasket = ReactiveCommand.CreateFromTask(async () =>
                await navigationService.NavigateAsync(typeof(IBasketViewModel)));

            BasketItemsCount = basketItemsService.ItemsCount;

            basketItemsService.ItemsCountChange
                .Select(x => x.ToString())
                .Subscribe(x => BasketItemsCount = x);
        }

        public string BasketItemsCount
        {
            get => _basketItemsCount;
            set => this.RaiseAndSetIfChanged(ref _basketItemsCount, value);
        }

        public override string Title => SelectedFood.Name;

        public IFoodViewModel SelectedFood { get; }

        public IBasketItemViewModel CurrentBasketItem
        {
            get => _currentBasketItemViewModel;
            private set => this.RaiseAndSetIfChanged(ref _currentBasketItemViewModel, value);
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