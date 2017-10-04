using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Common.DataTransferObjects
{
    public class UserDto
    {
	    public UserProfileDto Profile { get; set; }

	    public IEnumerable<OrderDto> Orders { get; set; }
    }
}
