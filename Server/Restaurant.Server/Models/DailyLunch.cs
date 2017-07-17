using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class DailyLunch : BaseEntity
    {
        public DateTime DateTime { get; set; }

        [NotMapped]
        public virtual decimal TotalAmount => AdditionalAmount + Amount;

        public decimal AdditionalAmount { get; set; }

        public decimal Amount { get; set; }

        public string Decsription { get; set; }

        public string Reciept { get; set; }

        public string QRCode { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}
