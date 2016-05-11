using ReactiveUI;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class FoodsViewModel : ReactiveObject, INavigatableViewModel
    {
        public MainViewModel MainViewModel { get; set; }
        public ReactiveCommand<object> OpenOrder { get; set; }

        public FoodsViewModel(INavigatableScreen screen = null)
        {
            OrderableFoods = new ReactiveList<Order>();
            OpenOrder = ReactiveCommand.Create();
            NavigationScreen = (screen ?? Locator.Current.GetService<INavigatableScreen>());
            MainViewModel = Locator.Current.GetService<MainViewModel>();

            var foods = Global.AuthenticationManager.AuthenticatedApi.GetFoods();
            foods.ToObservable()
            .Do(x =>
            {
                var orders = x.Select(f => new Order {Food = f, Id = Guid.NewGuid()});
                OrderableFoods.AddRange(orders);
            })
            .Subscribe();

            OpenOrder.Do(_ =>
                {
                    NavigationScreen.Navigation.Navigate.Execute(Locator.Current.GetService<BasketViewModel>());
                }).Subscribe();

        }
        public INavigatableScreen NavigationScreen
        {
            get; set;
        }

        public string Title => "Foods";

        public ReactiveList<Order> OrderableFoods { get; set; }
    }
}
