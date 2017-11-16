using System;
using System.Linq;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;
using Restaurant.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class BasketViewModel : BaseViewModel, IBasketViewModel
    {
        private ReactiveList<IOrderViewModel> _orders = new ReactiveList<IOrderViewModel>();
        private string _ordersCount;

        public BasketViewModel()
        {
            this.WhenAnyValue(x => x.Orders.Count).Subscribe(x => { OrdersCount = x == 0 ? null : x.ToString(); });
        }

        public ReactiveList<IOrderViewModel> Orders
        {
            get => _orders;
            set => this.RaiseAndSetIfChanged(ref _orders, value);
        }

        public string OrdersCount
        {
            get => _ordersCount;
            set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
        }

        public void AddOrder(IOrderViewModel order)
        {
            _orders.Add(order);

            var groupedOrders = _orders
                .GroupBy(x => x.Food)
                .Select(orders => new OrderViewModel(orders.Key, orders.Sum(s => s.Quantity)));

            Orders = new ReactiveList<IOrderViewModel>(groupedOrders);
        }

        public override string Title => "Your basket";
    }
}