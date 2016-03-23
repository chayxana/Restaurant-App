using ReactiveUI;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Restaurant.ViewModels
{
    public class FoodsViewModel : ReactiveObject, INavigatableViewModel
    {
        public MainViewModel MainViewModel { get; set; }
        public ReactiveCommand<object> OpenOrder { get; set; }
        public FoodsViewModel(INavigatableScreen screen = null)
        {
            NavigationScreen = (screen ?? Locator.Current.GetService<INavigatableScreen>());
            MainViewModel = Locator.Current.GetService<MainViewModel>();
            Foods = new ReactiveList<Food>
            {
                //new Food { Id = Guid.NewGuid(), Name = "Yogourt", Description = "Description of yogourt", ImageUrl="yogurt.jpg"},
                new Food { Id = Guid.NewGuid(), Name = "Shashlik", Description = "Description of shashlik", ImageUrl="shashlik.jpg", Price = 9},
                new Food { Id = Guid.NewGuid(), Name = "Rassolnik", Description = "Description of rassolnik", ImageUrl="rassolnik.jpg", Price= 3.5M},
                new Food { Id = Guid.NewGuid(), Name = "Shashlik", Description = "Description of shashlik", ImageUrl="shashlik.jpg", Price= 8.5M},
                new Food { Id = Guid.NewGuid(), Name = "Rassolnik", Description = "Description of rassolnik", ImageUrl="rassolnik.jpg", Price = 3.5M}
            };
            OpenOrder = ReactiveCommand.Create();
            OpenOrder.Subscribe(_ => 
            {
                NavigationScreen.Navigation.Navigate.Execute(Locator.Current.GetService<BasketViewModel>());
            });

        }
        public INavigatableScreen NavigationScreen
        {
            get; set;
        }

        public string Title
        {
            get
            {
                return "Foods";
            }
        }

        public ReactiveList<Food> Foods { get; set; }
    }
}
