using System;
using Newtonsoft.Json;

namespace Menu.API.DataTransferObjects
{
    public class FoodPictureDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("filePath")]
        public string FilePath { get; set; }
    }
}