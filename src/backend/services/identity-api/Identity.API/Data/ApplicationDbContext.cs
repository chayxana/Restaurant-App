using System.Diagnostics.CodeAnalysis;
using Identity.API.Model;
using Identity.API.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.UserProfile)
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey<UserProfile>(x => x.UserId);
        }
    }
}
