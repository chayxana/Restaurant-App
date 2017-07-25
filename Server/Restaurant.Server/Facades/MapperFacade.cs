using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Restaurant.Server.Abstractions.Facades;
using Restaurant.Server.Models;

namespace Restaurant.Server.Facades
{
    public class MapperFacade : IMapperFacade
    {
        public TDestination Map<TDestination, TSource>(TSource source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}