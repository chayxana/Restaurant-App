namespace Menu.API.Data
{
    using Menu.API.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>(b =>
            {
                b.HasMany(x => x.Foods).WithOne(x => x.Category)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.ToTable("Categories");
            });

            modelBuilder.Entity<FoodPicture>()
                .HasOne(x => x.Food)
                .WithMany(x => x.Pictures)
                .HasForeignKey(x => x.FoodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}