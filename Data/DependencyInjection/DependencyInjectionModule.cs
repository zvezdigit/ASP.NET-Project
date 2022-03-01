using Data;
using JoinMyCarTrip.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.DependencyInjection
{
    public static class DependencyInjectionModule
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection serviceCollection, string connectionString)
        {
            return serviceCollection
                .AddScoped<IRepository, Repository>()
                .AddDbContext<JoinMyCarTripDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
