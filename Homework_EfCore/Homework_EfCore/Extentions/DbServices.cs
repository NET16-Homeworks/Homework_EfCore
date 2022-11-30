using Microsoft.EntityFrameworkCore;
using Homework_EfCore.Contexts;
using Homework_EfCore.Interfaces;
using Homework_EfCore.Services;

namespace Homework_EfCore.Extentions
{
    public static class DbServices
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUserServices, UserServices>();
            //services.AddScoped<IDBService, DBService>();

            return services;
        }
    }
}
