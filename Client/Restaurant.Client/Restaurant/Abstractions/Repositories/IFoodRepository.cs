using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DataTransferObjects;

namespace Restaurant.Abstractions.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<FoodDto>> GetAllAsync(int? page = null, int? max = null);

        Task<FoodDto> Get(Guid id);
    }
}
