using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Server.Models
{
    public class FavoriteFood : BaseEntity
    {
        public virtual Guid FoodId { get; set; }

        public virtual string UserId { get; set; }
    }
}
