using System;
using System.Collections.Generic;
using AutoMapper;
using Restaurant.DataTransferObjects;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Mappers
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
                    map => map.MapFrom(x => Mapper.Map<FoodDto>(x.Category)))
				.ForMember(x => x.Id, 
					map => map.MapFrom(x => x.Id.ToString("N")))
				.ForMember(x => x.CategoryId, 
					map => map.MapFrom(x => x.CategoryId.ToString("N")));

            CreateMap<FoodDto, Food>()
                .ForMember(x => x.Category, 
                    map => map.MapFrom(x => Mapper.Map<Category>(x.CategoryDto)))
			    .ForMember(x => x.Id, 
					map => map.MapFrom(x => string.IsNullOrEmpty(x.Id) ? Guid.NewGuid() : Guid.Parse(x.Id)))
				.ForMember(x => x.CategoryId, 
					map => map.MapFrom(x => string.IsNullOrEmpty(x.CategoryId) ? Guid.Empty : Guid.Parse(x.CategoryId)));

			CreateMap<Category, CategoryDto>()
				.ForMember(x => x.Id, 
					map => map.MapFrom(x => x.Id.ToString("N")));

            CreateMap<CategoryDto, Category>()
				.ForMember(x => x.Id,
					map => map.MapFrom(x => string.IsNullOrEmpty(x.Id) ? Guid.NewGuid() : Guid.Parse(x.Id)));

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
