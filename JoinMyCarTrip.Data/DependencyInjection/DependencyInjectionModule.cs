using Data;
using JoinMyCarTrip.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JoinMyCarTrip.Data.DependencyInjection
{
    public static class DependencyInjectionModule
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection
                .AddScoped<IRepository, Repository>()
                .AddDbContext<JoinMyCarTripDbContext>(options => options.UseSqlServer(connectionString));
                
            return serviceCollection;
        }
    }
}
