using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Abstractions.Repositories;
using Restaurant.Server.Models;

namespace Restaurant.Server.Repositories
{
    public class FoodRepository : RepositoryBase, IRepository<Food>
    {
        private readonly DatabaseContext _context;

        public FoodRepository(DatabaseContext context, ILogger<FoodRepository> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public void Create(Food entity)
        {
            _context.Add(entity);
        }

        public void Update(Food entity)
        {
            _context.Update(entity);
        }

        public void Delete(Food entity)
        {
            _context.Remove(entity);
        }

        public Food Get(Guid id)
        {
            return _context.Foods
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
        }

        public IQueryable<Food> GetAll()
        {
            return _context.Foods;
        }
    }
}
