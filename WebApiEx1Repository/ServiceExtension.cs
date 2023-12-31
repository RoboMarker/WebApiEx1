using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEx1Repository.Context;
using WebApiEx1Repository.Interface;
using WebApiEx1Repository.Repository;

namespace WebApiEx1Repository
{
    public static class ServiceExtension
    {
        private readonly static string _DefConnectionName = "localDB";
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString(_DefConnectionName)));
            return services;
        }
    }
}
