using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Restaurant.DataTransferObjects;
using Restaurant.Server.Models;

namespace Restaurant.Server.Mappers
{
    public class RestaurantModelsToDtoProfile : Profile
    {
        public RestaurantModelsToDtoProfile()
        {
            CreateMap<DailyEatingDto, DailyEating>()
                .ForMember(x => x.TotalAmount, map => map.Ignore())
                .ForMember(x => x.Orders, map => map.MapFrom(x => Mapper.Map<ICollection<Order>>(x.Orders)));

            CreateMap<Food, FoodDto>()
                .ForMember(x => x.CategoryDto, 
                    map => map.MapFrom(x => Mapper.Map<FoodDto>(x.Category)));

            CreateMap<FoodDto, Food>()
                .ForMember(x => x.Category, 
                    map => map.MapFrom(x => Mapper.Map<Category>(x.CategoryDto)));

            CreateMap<Category, CategoryDto>();

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
