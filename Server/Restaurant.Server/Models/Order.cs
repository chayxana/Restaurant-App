using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class Order : BaseEntity
    {
        public DateTime DateTime { get; set; }
        
        public Guid EatingId { get; set; }
        
        public string UserId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
