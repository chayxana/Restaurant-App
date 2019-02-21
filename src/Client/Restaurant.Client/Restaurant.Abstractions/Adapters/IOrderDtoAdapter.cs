using System.Collections.Generic;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Adapters
{
    public interface IOrderDtoAdapter
    {
        OrderDto GetOrderFromOrderViewModels(IEnumerable<IBasketItemViewModel> orderViewModels);
    }
}