using Logistic.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistic.WebUI.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LogisticContext>(x => x.UseSqlServer(connection));
        }
    }
}
