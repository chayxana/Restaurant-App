using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Models;

namespace Restaurant.Server.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;

        protected RepositoryBase(DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<bool> Commit()
        {
            if (_context.ChangeTracker.HasChanges())
            {
                try
                {
                    return await _context.SaveChangesAsync() == 1;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(GetHashCode(), ex, "DbContext Validation Errors!");
                    return false;
                }
                
            }
            _logger.LogDebug("No changes to commit!");
            return false;
        }
    }
}