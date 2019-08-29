using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using ReactiveUI;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.ViewModels
{
    public class FoodsViewModel : BaseViewModel
    {
        private readonly IDiagnosticsFacade _diagnosticsFacade;
        private readonly IFoodDetailViewModelFactory _foodDetailViewModelFactory;
        private readonly IFoodsApi _foodsApi;
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;

        private string _basketItemsCount;
        private ObservableCollection<FoodViewModel> _foods;
        private FoodViewModel _selectedFood;

        private bool foodsLoaded;

        internal FoodsViewModel()
        {
        }

        public FoodsViewModel(
            IMapper mapper,
            IDiagnosticsFacade diagnosticsFacade,
            IFoodsApi foodsApi,
            INavigationService navigationService,
            IFoodDetailViewModelFactory foodDetailViewModelFactory,
            IBasketItemsService basketItemsService)
        {
            _mapper = mapper;
            _diagnosticsFacade = diagnosticsFacade;
            _foodsApi = foodsApi;
            _navigationService = navigationService;
            _foodDetailViewModelFactory = foodDetailViewModelFactory;

            this.WhenAnyValue(x => x.SelectedFood)
                .Where(x => x != null)
                .Subscribe(async food => await NavigateToFoodDetail(food));

            BasketItemsCount = basketItemsService.ItemsCount;

            basketItemsService.ItemsCountChange
                .Select(x => x.ToString())
                .Subscribe(x => BasketItemsCount = x);

            GoToBasket =
                ReactiveCommand.CreateFromTask(
                    async () => await _navigationService.NavigateAsync(typeof(IBasketViewModel)));
        }


        public ObservableCollection<FoodViewModel> Foods
        {
            get => _foods;
            private set => this.RaiseAndSetIfChanged(ref _foods, value);
        }

        public FoodViewModel SelectedFood
        {
            get => _selectedFood;
            set => this.RaiseAndSetIfChanged(ref _selectedFood, value);
        }


        public ICommand GoToBasket { get; set; }

        public string BasketItemsCount
        {
            get => _basketItemsCount;
            set => this.RaiseAndSetIfChanged(ref _basketItemsCount, value);
        }

        public override string Title => "Foods";

        public async Task LoadFoods()
        {
            if (foodsLoaded)
                return;

            try
            {
                IsLoading = true;
                var foods = await _foodsApi.GetFoods();
                var foodsViewModel = _mapper.Map<IEnumerable<FoodViewModel>>(foods);
                Foods = new ObservableCollection<FoodViewModel>(foodsViewModel);
                foodsLoaded = true;

                IsLoading = false;
            }
            catch (Exception ex)
            {
                _diagnosticsFacade.TrackError(ex);
                Foods = new ObservableCollection<FoodViewModel>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task NavigateToFoodDetail(FoodViewModel food)
        {
            var viewModel = _foodDetailViewModelFactory.GetFoodDetailViewModel(food);
            await _navigationService.NavigateAsync(viewModel);
        }
    }
}