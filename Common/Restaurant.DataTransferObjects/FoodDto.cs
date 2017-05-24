using System;

namespace Restaurant.DataTransferObjects
{
    public class FoodDto
    {
        public FoodDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public decimal Price { get; set; }
    }
}
