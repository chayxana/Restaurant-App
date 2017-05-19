using System;

namespace Restaurant.DataTransferObjects
{
    public class FoodDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public decimal Price { get; set; }



        public string Test { get; set; }
    }
}
