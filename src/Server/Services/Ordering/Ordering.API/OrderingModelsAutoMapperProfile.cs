using System.Collections.Generic;
using AutoMapper;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Models;

namespace Ordering.API
{
    public class OrderingModelsAutoMapperProfile : Profile
    {
        public OrderingModelsAutoMapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.OrderItems,
                    map => map.MapFrom(x => Mapper.Map<IEnumerable<OrderItemDto>>(x.OrderItems)));

            CreateMap<OrderDto, Order>()
                .ForMember(x => x.OrderItems,
                    map => map.MapFrom(x => Mapper.Map<ICollection<OrderItem>>(x.OrderItems)));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
