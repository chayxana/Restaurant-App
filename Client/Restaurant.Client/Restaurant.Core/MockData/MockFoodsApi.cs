using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Restaurant.Abstractions.Api;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.MockData
{
	public class MockFoodsApi : IFoodsApi
	{
		public Task<IEnumerable<FoodDto>> GetFoods()
		{
			return Task.FromResult(Data.Foods);
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