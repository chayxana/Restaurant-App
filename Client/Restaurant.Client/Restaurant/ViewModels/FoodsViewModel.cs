using ReactiveUI;
using ReactiveUI.Legacy;
using Restaurant.Models;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows.Input;
using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
    public class FoodsViewModel : ReactiveObject, INavigatableViewModel
    {
        public MainViewModel MainViewModel { get; set; }

        public ICommand OpenOrder { get; set; }

        public FoodsViewModel()
        {
            MainViewModel = Locator.Current.GetService<MainViewModel>();

            var foods = Global.AuthenticationManager.AuthenticatedApi.GetFoods();
            foods.ToObservable()
            .Do(x =>
            {
                var orders = x.Select(f => new Order { Food = f, Id = Guid.NewGuid() });
                OrderableFoods.AddRange(orders);
            })
            .Subscribe();

            //OpenOrder.Do(_ =>
            //    {
            //        //NavigationScreen.Navigation.Navigate.Execute(Locator.Current.GetService<BasketViewModel>());
            //    }).Subscribe();

        }

        public string Title => "Foods";

        public ReactiveList<Order> OrderableFoods { get; set; }
    }
}
