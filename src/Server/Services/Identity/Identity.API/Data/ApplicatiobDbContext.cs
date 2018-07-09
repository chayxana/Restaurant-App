using Identity.API.Model;
using Identity.API.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Identity.API.Data
{
    public class ApplicatiobDbContext : IdentityDbContext<ApplicationUser>
    {
    }
}
