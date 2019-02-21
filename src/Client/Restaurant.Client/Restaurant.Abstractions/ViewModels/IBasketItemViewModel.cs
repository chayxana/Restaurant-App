namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketItemViewModel
    {
        IFoodViewModel Food { get; }
        decimal Quantity { get; set; }
        decimal TotalPrice { get; }
	    string TotalPriceAnimated { get; set; }
    }
}