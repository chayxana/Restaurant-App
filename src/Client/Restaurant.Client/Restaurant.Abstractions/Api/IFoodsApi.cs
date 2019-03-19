using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Refit;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
    public interface IFoodsApi : IApi
    {
        [Get("/api/Foods/GetAll/")]
        Task<IEnumerable<FoodDto>> GetFoods(int count = 5, int skip = 0);

        [Get("/api/Foods/{id}")]
        Task<FoodDto> GetFood(Guid id);

        [Post("/api/Foods")]
        Task Create([Body] FoodDto food);

        [Post("/api/Foods/UploadFile")]
        [Multipart]
        Task UploadFile(Stream file, string foodId);

        [Put("/api/Foods/{id}")]
        Task Update(Guid id, [Body] FoodDto food);

        [Delete("/api/Foods/{id}")]
        Task Remove(Guid id);
    }
}