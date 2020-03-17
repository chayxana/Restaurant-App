using System.ComponentModel;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketItemViewModel : INotifyPropertyChanged
    {
        IFoodViewModel Food { get; }
        decimal Quantity { get; set; }
        decimal TotalPrice { get; }
	    string TotalPriceAnimated { get; set; }
    }
}