using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1Repository.Data;

namespace WebApiEx1Repository.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        // 添加无参构造函数
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
