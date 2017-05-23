using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Model;

namespace Restaurant.ViewModels
{
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        public MainViewModel()
        {
            
        }

        public UserInfoDto User { get; set; }

        public FoodsViewModel FoodViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }

        public IDetailedScreen DetailScreen { get; set; }
        
        public string Title => "Main";
    }

    public interface IDetailedScreen
    {
        DetailState DetailState { get; set; }
    }

    public class DetailState : ReactiveObject
    {

        //public ReactiveUI.Legacy.ReactiveCommand<INavigatableViewModel> MoveToDetail { get; set; }
    }
}
