using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Core.MockData
{
    [ExcludeFromCodeCoverage]
    public class MockFoodsApi : IFoodsApi
    {
        public async Task<IEnumerable<FoodDto>> GetFoods(int count = 10, int skip = 0)
        {
            await Task.Delay(1000);
            return Data.Foods;
        }

        public Task<FoodDto> GetFood(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Create(FoodDto food)
        {
            throw new NotImplementedException();
        }

        public Task UploadFile(Stream file, string foodId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, FoodDto food)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}