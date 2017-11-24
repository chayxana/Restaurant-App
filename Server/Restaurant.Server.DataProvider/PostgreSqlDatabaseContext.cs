using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Server.DataProvider
{
    [ExcludeFromCodeCoverage]
    public class PostgreSqlDatabaseContext : DatabaseContext
    {
        public PostgreSqlDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
