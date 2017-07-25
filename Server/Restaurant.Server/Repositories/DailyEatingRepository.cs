using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Abstractions.Repositories;
using Restaurant.Server.Models;

namespace Restaurant.Server.Repositories
{
    public class DailyEatingRepository : RepositoryBase, IRepository<DailyEating>
    {
        private readonly DatabaseContext _context;

        public DailyEatingRepository(DatabaseContext context, ILogger<DailyEatingRepository> logger) 
            : base(context, logger)
        {
            _context = context;
        }

        public void Create(DailyEating entity)
        {
            _context.Add(entity);
        }

        public void Update(DailyEating entity)
        {
            _context.Update(entity);
        }

        public void Delete(DailyEating entity)
        {
            _context.Remove(entity);
        }

        public DailyEating Get(Guid id)
        {
            return _context.Find<DailyEating>(id);
        }

        public IQueryable<DailyEating> GetAll()
        {
            return _context.DailyEatings;
        }
    }
}
