using System.Threading.Tasks;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;
using System;
using System.Reactive.Linq;

namespace Restaurant.Core.ViewModels
{
	public class BasketItemViewModel : ReactiveObject, IBasketItemViewModel
	{
		private decimal _quantity = 1;
		private string _totalPriceAnimated;

		public BasketItemViewModel(IFoodViewModel food)
		{
			Food = food;

			// Animating Total Price in UI
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

		public IFoodViewModel Food { get; }

		public decimal Quantity
		{
			get => _quantity;
			set
			{
				this.RaiseAndSetIfChanged(ref _quantity, value);
				this.RaisePropertyChanged(nameof(TotalPrice));
			}
		}

		public decimal TotalPrice => Quantity * Food.Price;

		public string TotalPriceAnimated
		{
			get => _totalPriceAnimated;
			set => this.RaiseAndSetIfChanged(ref _totalPriceAnimated, value);
		}
	}
}