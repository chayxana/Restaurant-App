using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class Order : BaseEntity
    {
        public DateTime DateTime { get; set; }
        
        public Guid DailyLunchId { get; set; }

        public virtual DailyLunch DailyLunch { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
