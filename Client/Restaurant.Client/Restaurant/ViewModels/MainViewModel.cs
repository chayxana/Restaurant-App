using ReactiveUI;
using Restaurant.Model;
using Splat;
using System;
using System.Reactive.Linq;

namespace Restaurant.ViewModels
{

    public class MainViewModel : ReactiveObject, INavigatableViewModel
    {
        public UserInfo User { get; set; }

        public FoodsViewModel FoodViewModel { get; set; }

        public BasketViewModel BasketViewModel { get; set; }

        public MainViewModel(UserInfo user, IDetailedScreen screen = null)
        {
            DetailScreen = (screen ?? Locator.Current.GetService<IDetailedScreen>());
            Locator.CurrentMutable.RegisterConstant(this, typeof(MainViewModel));
            Locator.CurrentMutable.RegisterConstant(new BasketViewModel(), typeof(BasketViewModel));
            Locator.CurrentMutable.RegisterConstant(new FoodsViewModel(), typeof(FoodsViewModel));
            FoodViewModel = Locator.Current.GetService<FoodsViewModel>();
            BasketViewModel = Locator.Current.GetService<BasketViewModel>();
            User = user;
        }
        public IDetailedScreen DetailScreen { get; set; }

        public INavigatableScreen NavigationScreen
        {
            get;
        }

        public string Title
        {
            get
            {
                return "Main";
            }
        }
    }

    public interface IDetailedScreen
    {
        DetailState DetailState { get; set; }
    }

    public class DetailState : ReactiveObject
    {

        //public ReactiveUI.Legacy.ReactiveCommand<INavigatableViewModel> MoveToDetail { get; set; }

        public DetailState()
        {
            //MoveToDetail = new ReactiveUI.Legacy.ReactiveCommand<INavigatableViewModel>(Observable.Return(true), x =>
            //{
            //    var vm = x as INavigatableViewModel;
            //    if (vm == null)
            //    {
            //        throw new Exception("Navigate must be called on an INavigatableViewModel");
            //    }
            //    return Observable.Return<INavigatableViewModel>(vm);
            //});
        }
    }
}
