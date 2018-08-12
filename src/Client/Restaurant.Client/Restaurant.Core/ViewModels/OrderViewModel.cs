using System.Threading.Tasks;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using System;
using System.Reactive.Linq;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.ViewModels
{
	public class OrderViewModel : ReactiveObject, IOrderViewModel
	{
		private decimal _quntity = 1;
		private string _totalPriceAnimated;

		public OrderViewModel(IFoodViewModel food)
		{
			Food = food;

			this.WhenAnyValue(x => x.Quantity)
				.Select(_ => TotalPrice)
				.Subscribe(async totalPrice =>
				{
					var j = totalPrice - 15;
					j = j <= 0 ? 0 : j;
					for (var i = j; i <= totalPrice; i++)
					{
						await Task.Delay(5);
						TotalPriceAnimated = $"{i:C}";
					}
				});
		}

		public OrderViewModel(IFoodViewModel food, decimal quntity)
		{
			Food = food;
			Quantity = quntity;
		}

		public IFoodViewModel Food { get; }

		public decimal Quantity
		{
			get => _quntity;
			set
			{
				this.RaiseAndSetIfChanged(ref _quntity, value);
				this.RaisePropertyChanged(nameof(TotalPrice));
			}
		}

		public decimal TotalPrice => Quantity * Food.Price;

		public string TotalPriceAnimated
		{
			get => _totalPriceAnimated;
			set => this.RaiseAndSetIfChanged(ref _totalPriceAnimated, value);
		}

		public IOrderViewModel Clone()
		{
			return (IOrderViewModel)MemberwiseClone();
		}
	}
}