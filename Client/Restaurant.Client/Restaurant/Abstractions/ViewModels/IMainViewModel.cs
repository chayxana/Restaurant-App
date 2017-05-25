using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
    public interface IMainViewModel : INavigatableViewModel
    {
        FoodsViewModel FoodViewModel { get; set; }
        OrderViewModel OrderViewModel { get; set; }
    }
}