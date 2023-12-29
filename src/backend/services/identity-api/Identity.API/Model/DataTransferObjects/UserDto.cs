namespace Identity.API.Model.DataTransferObjects
{
    public class UserDto
    {
        public string Email { get; set; }

        public UserProfileDto Profile { get; set; }
    }
}