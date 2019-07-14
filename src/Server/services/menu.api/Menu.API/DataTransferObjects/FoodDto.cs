using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Menu.API.DataTransferObjects
{
    public class FoodDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("pictures")]
        public List<FoodPictureDto> Pictures { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("category")]
        public CategoryDto CategoryDto { get; set; }

        [JsonProperty("categoryId")]
        public Guid CategoryId { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        public FoodDto Clone()
        {
            return (FoodDto)this.MemberwiseClone();
        }
    }
}