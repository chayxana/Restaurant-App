using ReactiveUI;
using Restaurant.Model;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class Order
    {
        public Food Food { get; set; }
    }

    public class MainViewModel : ReactiveObject, INavigatableViewModel
    {
        public ClientUser User { get; set; }

        public FoodsViewModel FoodViewModel { get; set; }

        private int ordersCount;

        public int OrdersCount
        {
            get { return ordersCount; }
            set { this.RaiseAndSetIfChanged(ref ordersCount, value); }
        }

        public ReactiveList<Order> Orders { get; set; }

        public MainViewModel(ClientUser user)
        {
            Locator.CurrentMutable.RegisterConstant(this, typeof(MainViewModel));
            User = user;
            Orders = new ReactiveList<Order>();
            FoodViewModel = new FoodsViewModel();     
            this.WhenAnyValue(x => x.Orders.Count).Subscribe(x => OrdersCount = x);
            Locator.CurrentMutable.RegisterConstant(FoodViewModel, typeof(FoodsViewModel));
        }

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
}
