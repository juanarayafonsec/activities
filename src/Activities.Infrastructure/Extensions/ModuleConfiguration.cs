using Activities.Application.Interfaces;
using Activities.Domain.Entity;
using Activities.Infrastructure.Identity;
using Activities.Infrastructure.Persistance;
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

        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}

