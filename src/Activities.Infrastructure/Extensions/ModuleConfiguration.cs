using Activities.Domain.Interfaces;
using Activities.Infrastructure.Persistance.Context;
using Activities.Infrastructure.Persistance.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Activities.Application.Extensions;
public static class ModuleConfiguration
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfigurationManager conf)
    {
        services.AddDbContext<ActivityContext>(options =>
        {
            options.UseSqlite(conf.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IActivityRepository,ActivityRepository>();

        return services;
    }
}

