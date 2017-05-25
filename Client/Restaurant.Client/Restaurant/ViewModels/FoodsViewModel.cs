using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Repositories;
using Restaurant.DataTransferObjects;
using Restaurant.Models;
using Splat;

namespace Restaurant.ViewModels
{
    public class FoodsViewModel : ReactiveObject, INavigatableViewModel
    {
        private readonly IFoodRepository _foodRepository;

        public FoodsViewModel(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        private ObservableCollection<FoodDto> _foods;
        public ObservableCollection<FoodDto> Foods
        {
            get => _foods;
            private set => this.RaiseAndSetIfChanged(ref _foods, value);
        }

        private FoodDto _selectedFood;

        public FoodDto SelectedFood
        {
            get => _selectedFood;
            set => this.RaiseAndSetIfChanged(ref _selectedFood, value);
        }

        public string Title => "Foods";

        public async Task LoadFoods()
        {
            var foods = await _foodRepository.GetAllAsync();
            Foods = new ObservableCollection<FoodDto>(foods);
        }

    }
}
