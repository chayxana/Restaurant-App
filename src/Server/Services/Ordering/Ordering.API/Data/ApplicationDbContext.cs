using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Server.Api.Models;

namespace Ordering.API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>(b =>
            {
                b.HasKey(oi => new { oi.FoodId, oi.OderId });
                b.ToTable("OrderItems");
            });
        }
    }

}
