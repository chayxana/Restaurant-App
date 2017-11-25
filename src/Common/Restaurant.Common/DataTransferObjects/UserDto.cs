using System.Collections.Generic;

namespace Restaurant.Common.DataTransferObjects
{
    public class UserDto
    {
        public UserProfileDto Profile { get; set; }

        public IEnumerable<OrderDto> Orders { get; set; }
    }
}