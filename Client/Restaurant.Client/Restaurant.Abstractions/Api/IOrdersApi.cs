using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
    public interface IOrdersApi : IApi
    {
        Task<IEnumerable<OrderDto>> GetAll();

        Task Create(OrderDto dto);
    }
}