using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class Category : BaseEntity
    {
        public virtual ICollection<Food> Foods { get; set; }
        
        public string Name { get; set; }
    }
}
