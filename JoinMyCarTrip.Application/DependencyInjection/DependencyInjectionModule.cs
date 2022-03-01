using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.DependencyInjection
{
    public static class DependencyInjectionModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<ICarTripService, CarTripService>()
                .AddDatabaseServices(configuration.GetConnectionString("JoinMyCarTrip")); // TODO: AST
        }
    }
}
