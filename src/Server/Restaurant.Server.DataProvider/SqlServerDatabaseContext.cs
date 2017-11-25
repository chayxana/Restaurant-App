using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Server.DataProvider
{
    [ExcludeFromCodeCoverage]
    public class SqlServerDatabaseContext : DatabaseContext
    {
        public SqlServerDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
