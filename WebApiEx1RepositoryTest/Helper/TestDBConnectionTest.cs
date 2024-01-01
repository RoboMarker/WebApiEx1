using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1Repository.Context;
using Moq;

namespace WebApiEx1Repository.Helper
{
    public class TestDBConnectionTest
    {
        [Fact]
        public void checkConnection()
        {
            AppDbContext context = new AppDbContext(TestDBConnection.GetConnection());
            if (context != null)
            { 
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
            }
            Assert.NotNull(context);
            Assert.Empty(context.User);
        }
    }
}
