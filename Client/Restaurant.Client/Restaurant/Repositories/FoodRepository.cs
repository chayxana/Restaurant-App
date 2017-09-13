using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Abstractions.Repositories;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Model;

namespace Restaurant.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly IRestaurantApi _api;

        public FoodRepository(IRestaurantApi api)
        {
            _api = api;
        }

        public Task<IEnumerable<FoodDto>> GetAllAsync(int? page = null, int? max = null)
        {
            return _api.GetFoods();
        }

        public Task<FoodDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(FoodDto food)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(FoodDto food)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(FoodDto food)
        {
            throw new NotImplementedException();
        }
    }
}
