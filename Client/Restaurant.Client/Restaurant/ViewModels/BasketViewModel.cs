using System;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.ViewModels
{
	public class BasketViewModel : BaseViewModel, IBasketViewModel
	{
	    private ReactiveList<OrderViewModel> _orders;
		public ReactiveList<OrderViewModel> Orders
		{
			get => _orders;
			set => this.RaiseAndSetIfChanged(ref _orders, value);
		}

		private string _ordersCount;
		public string OrdersCount
		{
			get => _ordersCount;
			set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
		}

		public override string Title => "Your basket";

		public BasketViewModel()
		{
		    Orders = new ReactiveList<OrderViewModel>();

			this.WhenAnyValue(x => x.Orders.Count).Subscribe(x =>
			{
				OrdersCount = x == 0 ? null : x.ToString();
			});
		}
        
	}
}
