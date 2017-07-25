using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class Category : BaseEntity
    {   
        [Required]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
