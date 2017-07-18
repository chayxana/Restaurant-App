using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class Food : BaseEntity
    {
        public virtual Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Recept { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }
    }
}
