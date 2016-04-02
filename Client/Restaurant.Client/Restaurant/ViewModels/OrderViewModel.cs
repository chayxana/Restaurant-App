using ReactiveUI;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using System;

namespace Restaurant.ViewModels
{
    public class BasketViewModel : ReactiveObject, INavigatableViewModel
    {
        private ReactiveList<Order> orders;
        public ReactiveList<Order> Orders
        {
            get { return orders; }
            set { this.RaiseAndSetIfChanged(ref orders, value); }
        }

        private int ordersCount;
        public int OrdersCount
        {
            get { return ordersCount; }
            set { this.RaiseAndSetIfChanged(ref ordersCount, value); }
        }

        public INavigatableScreen NavigationScreen { get; }

        public string Title => "Your basket";

        public BasketViewModel(INavigatableScreen navigationScreen = null)
        {
            NavigationScreen = navigationScreen;
            Orders = new ReactiveList<Order>();
            this.WhenAnyValue(x => x.Orders.Count)
                .Subscribe(x =>
                {
                    OrdersCount = x;
                });
        }
    }
}
