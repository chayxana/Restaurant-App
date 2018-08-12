using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IOrderViewModel
    {
        IFoodViewModel Food { get; }
        decimal Quantity { get; set; }
        decimal TotalPrice { get; }
	    string TotalPriceAnimated { get; set; }
		IOrderViewModel Clone();
    }
}