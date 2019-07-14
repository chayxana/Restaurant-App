using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Menu.API.Models
{
    public class Food
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Recept { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Currency { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<FoodPicture> Pictures { get; set; }
    }
}