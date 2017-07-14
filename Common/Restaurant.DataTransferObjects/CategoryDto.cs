using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataTransferObjects
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FoodDto> Foods { get; set; }
    }
}
