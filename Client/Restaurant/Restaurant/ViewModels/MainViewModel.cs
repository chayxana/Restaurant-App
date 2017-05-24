using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Repositories;
using Restaurant.Model;

namespace Restaurant.ViewModels
{
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        private readonly IUserRepository _userRepository;

        public MainViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            OnLoad();
        }

        public UserInfoDto User { get; set; }

        public FoodsViewModel FoodViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }

        public IDetailedScreen DetailScreen { get; set; }
        
        public string Title => "Main";

        private async void OnLoad()
        {
            User = await _userRepository.GetUserInfo();
        }
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
