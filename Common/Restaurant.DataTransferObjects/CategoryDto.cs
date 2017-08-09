using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Restaurant.DataTransferObjects
{
    public class CategoryDto
    {
		[JsonProperty("id")]
        public Guid Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("color")]
        public string Color { get; set; }
    }
}
