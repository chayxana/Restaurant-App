using System;

namespace Restaurant.DataTransferObjects
{
    public class NotificationDto
    {
        public DateTime Date { get; set; }

        public string PriceForTransport { get; set; }

        public string AdditionalPriceForEveryOne { get; set; }

        public string TotalAmount { get; set; }
    }
}
