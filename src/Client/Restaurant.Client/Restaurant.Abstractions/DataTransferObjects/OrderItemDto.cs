using System;

namespace Restaurant.Abstractions.DataTransferObjects
{
    public class OrderItemDto
    {
        public decimal Quantity { get; set; }

        public Guid FoodId { get; set; }

        public Guid OderId { get; set; }
    }
}