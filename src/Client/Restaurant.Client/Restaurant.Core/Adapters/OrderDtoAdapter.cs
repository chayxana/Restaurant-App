using System;
using System.Collections.Generic;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.Adapters
{
    public class OrderDtoAdapter : IOrderDtoAdapter
    {
        private readonly IAutoMapperFacade _autoMapperFacade;

        public OrderDtoAdapter(IAutoMapperFacade autoMapperFacade)
        {
            _autoMapperFacade = autoMapperFacade;
        }

        public OrderDto GetOrderFromOrderViewModels(ReactiveList<IOrderViewModel> orderViewModels)
        {
            var orderItems = _autoMapperFacade.Map<IEnumerable<OrderItemDto>>(orderViewModels);
            return new OrderDto()
            {
                DateTime = DateTime.Now,
                OrderItems = new List<OrderItemDto>(orderItems)
            };
        }
    }
}
