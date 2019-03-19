using System;
using System.Collections.Generic;

namespace Restaurant.Abstractions.DataTransferObjects
{
    public class OrderDto
    {
        public OrderDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string UserId { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}