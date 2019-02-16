using System.Collections.Generic;

namespace Restaurant.Common.DataTransferObjects
{
    public class UserDto
    {
		public string Email { get; set; }

        public UserProfileDto Profile { get; set; }
    }
}