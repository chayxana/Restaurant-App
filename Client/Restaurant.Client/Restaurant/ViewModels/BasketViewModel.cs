using System;
using System.Linq;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.ViewModels
{
	public class BasketViewModel : BaseViewModel, IBasketViewModel
	{
		private ReactiveList<OrderViewModel> _orders = new ReactiveList<OrderViewModel>();
		private string _ordersCount;

		public BasketViewModel()
		{
			this.WhenAnyValue(x => x.Orders.Count).Subscribe(x =>
			{
				OrdersCount = x == 0 ? null : x.ToString();
			});
		}

		public ReactiveList<OrderViewModel> Orders
		{
			get => _orders;
			set => this.RaiseAndSetIfChanged(ref _orders, value);
		}

		public string OrdersCount
		{
			get => _ordersCount;
			set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
		}

		public void AddOrder(OrderViewModel order)
		{
			_orders.Add(order);

			var groupedOrders = _orders
				.GroupBy(x => x.Food)
				.Select(orders => new OrderViewModel(orders.Key, orders.Sum(s => s.Quantity)));

			Orders = new ReactiveList<OrderViewModel>(groupedOrders);
		}

		public override string Title => "Your basket";
	}
}