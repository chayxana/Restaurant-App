using System;
using System.Collections.Generic;

namespace Restaurant.DataTransferObjects
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }
    }
}
