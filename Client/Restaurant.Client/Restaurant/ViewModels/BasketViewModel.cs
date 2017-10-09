using System;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions.Services;

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

		private string _ordersCount;
		public string OrdersCount
		{
			get => _ordersCount;
			set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
		}

		public string Title => "Your basket";

		public BasketViewModel(INavigationService navigationService)
		{
			Orders = new ReactiveList<OrderViewModel>();
			this.WhenAnyValue(x => x.Orders.Count).Subscribe(x =>
			{
				OrdersCount = x == 0 ? null : x.ToString();
			});

			DoneCommand = ReactiveCommand.Create(() =>
			{
				navigationService.PopModalAsync(true);
			});
		}

		public ICommand DoneCommand { get; set; }
	}
}
