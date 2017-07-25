using System;
using System.Collections.Generic;

namespace Restaurant.DataTransferObjects
{
    public class DailyEatingDto
    {
        public DateTime DateTime { get; set; }

        public decimal AdditionalAmount { get; set; }

        public decimal Amount { get; set; }

        public string Decsription { get; set; }

        public string Reciept { get; set; }

        public virtual ICollection<OrderDto> Orders { get; set; }
        
        public decimal TotalAmount => AdditionalAmount + Amount;
    }
}
