using System;
using System.Collections.Generic;

namespace Restaurant.DataTransferObjects
{
    public class OrderDto
    {
        public DateTime DateTime { get; set; }

        public Guid EatingId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
