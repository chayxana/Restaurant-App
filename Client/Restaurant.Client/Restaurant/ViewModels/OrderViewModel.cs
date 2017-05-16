using ReactiveUI;
using Restaurant.Models;
using System;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Services;

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


        public string Title => "Your basket";

        public BasketViewModel(INavigationService navigationService = null)
        {
            Orders = new ReactiveList<Order>();
            this.WhenAnyValue(x => x.Orders.Count)
                .Subscribe(x =>
                {
                    OrdersCount = x;
                });
        }
    }
}
