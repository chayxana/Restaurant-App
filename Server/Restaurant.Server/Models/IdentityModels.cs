using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;

namespace Restaurant.Server.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("RestaurantSqlConnection", throwIfV1Schema: false)
        {
            if (!Database.Exists())
            {
                Database.CreateIfNotExists();
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public HashSet<Order> Orders { get; set; }

        public HashSet<Food> Foods { get; set; }
    }
    
    public class Food
    {
        public Food()
        {
            Orders = new HashSet<Order>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
    
    public class Order
    {
        public Guid Id { get; set; }

        public virtual User User { get; set; }

        public virtual Food Food { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }
    }
}