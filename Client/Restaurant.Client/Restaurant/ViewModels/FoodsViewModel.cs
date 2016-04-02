using ReactiveUI;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;

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
            new ReactiveList<Food>
            {
                new Food { Id = Guid.NewGuid(), Name = "Shashlik", Description = "Description of shashlik", ImageUrl="shashlik.jpg", Price = 9},
                new Food { Id = Guid.NewGuid(), Name = "Rassolnik", Description = "Description of rassolnik", ImageUrl="rassolnik.jpg", Price= 3.5M},
                new Food { Id = Guid.NewGuid(), Name = "Shashlik", Description = "Description of shashlik", ImageUrl="shashlik.jpg", Price= 8.5M},
                new Food { Id = Guid.NewGuid(), Name = "Rassolnik", Description = "Description of rassolnik", ImageUrl="rassolnik.jpg", Price = 3.5M}
            }
            .ToObservable()
            .Do(x => OrderableFoods.Add( new Order { Food = x, Id = Guid.NewGuid() }))
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
