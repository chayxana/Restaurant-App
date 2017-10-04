using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<FoodDto>> GetAllAsync(int? page = null, int? max = null);

        Task<FoodDto> Get(Guid id);

        Task<bool> Create(FoodDto food);

        Task<bool> Update(FoodDto food);

        Task<bool> Delete(FoodDto food);
    }
}
