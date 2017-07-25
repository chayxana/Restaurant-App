using System;

namespace Restaurant.Server.Models
{
    public class Favorite
    {
        public string UserId { get; set; }

        public Guid FoodId { get; set; }
    }
}
