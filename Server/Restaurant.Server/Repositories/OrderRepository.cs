using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Abstractions.Repositories;
using Restaurant.Server.Models;

namespace Restaurant.Server.Repositories
{
    public class OrderRepository : RepositoryBase, IRepository<Order>
    {
        private readonly DatabaseContext _context;

        public OrderRepository(
            DatabaseContext context,
            ILogger<OrderRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        public void Create(Order entity)
        {
            _context.Add(entity);
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
        }

        public void Delete(Order entity)
        {
            _context.Remove(entity);
        }

        public Order Get(Guid id)
        {
            return _context.Orders
                .Where(x => x.Id == id)
                .Include(x => x.OrderItems)
                .FirstOrDefault();
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders;
        }
    }
}
