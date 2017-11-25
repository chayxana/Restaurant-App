using ReactiveUI;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Adapters
{
    public interface IOrderDtoAdapter
    {
        OrderDto GetOrderDto(ReactiveList<IOrderViewModel> orderViewModels);
    }
}