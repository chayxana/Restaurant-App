using System;
using Newtonsoft.Json;

namespace Restaurant.Abstractions.DataTransferObjects
{
    public class FoodDto
    {
        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("picture")] public string Picture { get; set; }

        [JsonProperty("price")] public decimal Price { get; set; }

        [JsonProperty("category")] public CategoryDto CategoryDto { get; set; }

        [JsonProperty("categoryId")] public Guid CategoryId { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }
    }
}