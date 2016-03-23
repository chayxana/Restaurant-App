using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Food Food { get; set; }
    }
}
