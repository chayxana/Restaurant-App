namespace Restaurant.DataTransferObjects
{
    public class UserInfoDto
    {
        public string Email { get; set; }

        public bool IsRegistered { get; set; }

        public string LoginProvider { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }
    }
}