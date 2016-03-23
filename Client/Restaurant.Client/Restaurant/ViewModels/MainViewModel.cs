using ReactiveUI;
using Restaurant.Model;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{

    public class MainViewModel : ReactiveObject, INavigatableViewModel
    {
        public ClientUser User { get; set; }

        public FoodsViewModel FoodViewModel { get; set; }

        public BasketViewModel BasketViewModel { get; set; }

        public MainViewModel(ClientUser user, IDetailedScreen screen = null)
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
        public ReactiveCommand<INavigatableViewModel> MoveToDetail { get; set; }

        public DetailState()
        {
            MoveToDetail = new ReactiveCommand<INavigatableViewModel>(Observable.Return(true), x =>
            {
                var vm = x as INavigatableViewModel;
                if (vm == null)
                {
                    throw new Exception("Navigate must be called on an INavigatableViewModel");
                }
                return Observable.Return<INavigatableViewModel>(vm);
            });
        }
    }
}
