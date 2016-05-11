using System;

namespace Restaurant.Models
{
    public class Food 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public decimal Price { get; set; }
    }
}
