namespace Restaurant.Server.Api.Models
{
    public class UserProfile : BaseEntity
    {
        public string Picture { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
