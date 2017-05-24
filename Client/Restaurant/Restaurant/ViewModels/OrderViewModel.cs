using System;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Services;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    public class OrderViewModel : ReactiveObject, INavigatableViewModel
    {
        private ReactiveList<Order> _orders;
        public ReactiveList<Order> Orders
        {
            get => _orders;
            set => this.RaiseAndSetIfChanged(ref _orders, value);
        }

        private int _ordersCount;
        public int OrdersCount
        {
            get => _ordersCount;
            set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
        }


        public string Title => "Your basket";

        public OrderViewModel(INavigationService navigationService = null)
        {
            Orders = new ReactiveList<Order>();
            this.WhenAnyValue(x => x.Orders.Count).Subscribe(x =>
            {
                OrdersCount = x;
            });
        }
    }
}
