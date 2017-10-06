using System;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Services;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    public class BasketViewModel : ReactiveObject, IBasketViewModel
    {
        private ReactiveList<OrderViewModel> _orders;
        public ReactiveList<OrderViewModel> Orders
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

        public BasketViewModel(INavigationService navigationService = null)
        {
            Orders = new ReactiveList<OrderViewModel>();
            this.WhenAnyValue(x => x.Orders.Count).Subscribe(x =>
            {
                OrdersCount = x;
            });
        }
    }
}
