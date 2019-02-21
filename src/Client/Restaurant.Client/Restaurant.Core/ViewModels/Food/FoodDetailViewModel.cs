using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Publishers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.ViewModels
{
    public class FoodDetailViewModel : BaseViewModel, IFoodDetailViewModel
    {
        private IBasketItemViewModel _currentBasketItemViewModel;

        public FoodDetailViewModel(
            IFoodViewModel selectedFood,
            IBasketItemViewModelPublisher basketItemViewModelPublisher,
            INavigationService navigationService)
        {
            SelectedFood = selectedFood;
            
            CurrentBasketItem = new BasketItemViewModel(SelectedFood);

            AddToBasket = ReactiveCommand.Create(() => 
                basketItemViewModelPublisher.Publish(CurrentBasketItem));

            GoToBasket = ReactiveCommand.CreateFromTask(async () =>
                await navigationService.NavigateAsync(typeof(IBasketViewModel)));
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