using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Models
{
    public class UserProfile : BaseEntity
    {
        public string Picture { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
