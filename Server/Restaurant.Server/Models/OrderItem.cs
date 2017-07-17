using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class OrderItem : BaseEntity
    {
        public decimal Quantity { get; set; }

        public Guid FoodId { get; set; }

        public virtual Food Food { get; set; }

        public Guid OderId { get; set; }

        public virtual  Order Order { get; set; }
    }
}
