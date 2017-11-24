using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
    public interface IOrdersApi : IApi
    {
        [Get("/api/Orders/GetAll")]
        Task<IEnumerable<OrderDto>> GetAll();

        [Post("/api/Orders/Create")]
        Task Create(OrderDto dto);
    }
}