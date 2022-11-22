using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Logistic.DAL.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Logistic.WebUI.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IPointRepository, PointRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<ITransportationRepository, TransportationRepository>();
        }
    }
}