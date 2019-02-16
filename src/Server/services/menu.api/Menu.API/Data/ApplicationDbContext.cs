using Menu.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(b =>
            {
                b.HasMany(x => x.Foods).WithOne(x => x.Category)
                                       .HasForeignKey(x => x.CategoryId)
                                       .OnDelete(DeleteBehavior.Cascade);
                b.ToTable("Categories");
            });
        }
    }
}