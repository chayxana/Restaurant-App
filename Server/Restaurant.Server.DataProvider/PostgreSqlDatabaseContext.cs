using Microsoft.EntityFrameworkCore;

namespace Restaurant.Server.DataProvider
{
    public class PostgreSqlDatabaseContext : DatabaseContext
    {
        public PostgreSqlDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
