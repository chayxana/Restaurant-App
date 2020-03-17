using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
    public interface IOrdersApi : IApi
    {
        [Get("/api/Orders")]
        Task<IEnumerable<OrderDto>> GetAll();

        [Post("/api/Orders")]
        Task Create(OrderDto dto);
    }
}