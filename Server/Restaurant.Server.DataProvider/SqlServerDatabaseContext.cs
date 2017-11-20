using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Server.DataProvider
{
    public class SqlServerDatabaseContext : DatabaseContext
    {
        public SqlServerDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
