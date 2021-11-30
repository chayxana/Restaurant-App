using System;
using System.Collections.Generic;
using AutoMapper;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.Adapters
{
    public class OrderDtoAdapter : IOrderDtoAdapter
    {
	    private readonly IDateTimeFacade _dateTimeFacade;
	    private readonly IMapper _mapper;

        public OrderDtoAdapter(
			IDateTimeFacade dateTimeFacade,
			IMapper mapper)
        {
	        _dateTimeFacade = dateTimeFacade;
	        _mapper = mapper;
        }

        public OrderDto GetOrderFromOrderViewModels(IEnumerable<IBasketItemViewModel> orderViewModels)
        {
            var orderItems = _mapper.Map<IEnumerable<OrderItemDto>>(orderViewModels);
            return new OrderDto
            {
                DateTime = _dateTimeFacade.Now,
                OrderItems = orderItems
            };
        }
    }
}
