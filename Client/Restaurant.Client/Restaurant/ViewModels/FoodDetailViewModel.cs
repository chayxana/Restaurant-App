using Restaurant.Common.DataTransferObjects;

namespace Restaurant.ViewModels
{
    public class FoodDetailViewModel : ViewModelBase
    {
        public FoodDto SelectedFood { get; }

        public FoodDetailViewModel(FoodDto selectedFood)
        {
            SelectedFood = selectedFood;
        }

        public override string Title => SelectedFood.Name;
    }
}
