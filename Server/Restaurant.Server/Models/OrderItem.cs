using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class OrderItem
    {
        public decimal Quantity { get; set; }

        public Guid FoodId { get; set; }
        
        public Guid OderId { get; set; }
    }
}
