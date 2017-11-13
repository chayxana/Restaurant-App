using Restaurant.Common.DataTransferObjects;

namespace Restaurant.ViewModels
{
    public interface IOrderViewModel
    {
        FoodDto Food { get; }
        decimal Quantity { get; set; }
        decimal TotalPrice { get; }
    }
}