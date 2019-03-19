using System.Collections.Generic;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Abstractions.Adapters
{
    public interface IOrderDtoAdapter
    {
        OrderDto GetOrderFromOrderViewModels(IEnumerable<IBasketItemViewModel> orderViewModels);
    }
}