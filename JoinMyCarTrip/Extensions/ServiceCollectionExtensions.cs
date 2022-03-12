using Data;
using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped<IRepository, Repository>()
                .AddScoped<ICarTripService, CarTripService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("JoinMyCarTrip");
            services.AddDbContext<JoinMyCarTripDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}