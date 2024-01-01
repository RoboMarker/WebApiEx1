using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1Repository.Context;

namespace WebApiEx1Repository.Helper
{
    public class TestDBConnection
    {
        public static DbContextOptions<AppDbContext> GetConnection()
        {

            DbContextOptions<AppDbContext> _dbContextOptions;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            _dbContextOptions=builder.UseSqlite(connection).Options;
            return _dbContextOptions;
        }

    }
}
