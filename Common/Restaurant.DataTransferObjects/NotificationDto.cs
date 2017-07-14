using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
