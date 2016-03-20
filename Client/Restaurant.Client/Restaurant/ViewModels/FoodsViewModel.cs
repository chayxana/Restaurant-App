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
        public FoodsViewModel()
        {
            MainViewModel = Locator.Current.GetService<MainViewModel>();
            Foods = new ReactiveList<Food>
            {
                //new Food { Id = Guid.NewGuid(), Name = "Yogourt", Description = "Description of yogourt", ImageUrl="yogurt.jpg"},
                new Food { Id = Guid.NewGuid(), Name = "Shashlik", Description = "Description of shashlik", ImageUrl="shashlik.jpg", Price = 9},
                new Food { Id = Guid.NewGuid(), Name = "Rassolnik", Description = "Description of rassolnik", ImageUrl="rassolnik.jpg", Price= 3.5M},
                new Food { Id = Guid.NewGuid(), Name = "Shashlik", Description = "Description of shashlik", ImageUrl="shashlik.jpg", Price= 8.5M},
                new Food { Id = Guid.NewGuid(), Name = "Rassolnik", Description = "Description of rassolnik", ImageUrl="rassolnik.jpg", Price = 3.5M}
            };
        }
        public INavigatableScreen NavigationScreen
        {
            get;
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
