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
        private readonly IFoodDetailViewModelFactory _foodDetailViewModelFactory;
        private readonly IMapper _mapper;
        private readonly IDiagnosticsFacade _diagnosticsFacade;
        private readonly IFoodsApi _foodsApi;
        private readonly INavigationService _navigationService;
        private ObservableCollection<FoodViewModel> _foods;
        private FoodViewModel _selectedFood;

        internal FoodsViewModel()
        {
        }

        public FoodsViewModel(
            IMapper mapper,
            IDiagnosticsFacade diagnosticsFacade,
            IBasketViewModel basketViewModel,
            IFoodsApi foodsApi,
            INavigationService navigationService,
            IFoodDetailViewModelFactory foodDetailViewModelFactory)
        {
            _mapper = mapper;
            _diagnosticsFacade = diagnosticsFacade;
            _foodsApi = foodsApi;
            _navigationService = navigationService;
            _foodDetailViewModelFactory = foodDetailViewModelFactory;

            this.WhenAnyValue(x => x.SelectedFood)
                .Where(x => x != null)
                .Subscribe(async food => await NavigateToFoodDetail(food));

            GoToBasket =
                ReactiveCommand.CreateFromTask(async () => await _navigationService.NavigateAsync(BasketViewModel));

            BasketViewModel = basketViewModel;
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

        public IBasketViewModel BasketViewModel { get; }

        public ICommand GoToBasket { get; set; }

        public override string Title => "Foods";

        private bool foodsLoaded = false;

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