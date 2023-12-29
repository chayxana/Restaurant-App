using Microsoft.AspNetCore.Identity;

namespace Identity.API.Model.Entities
{
    public sealed class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserProfile = new UserProfile();
        }
        public UserProfile UserProfile { get; set; }
    }
}