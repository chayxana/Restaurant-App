using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class Food : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Recept { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Currency { get; set; }
        
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
