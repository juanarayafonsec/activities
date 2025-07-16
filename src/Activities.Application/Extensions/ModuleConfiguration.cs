using Activities.Application.Messaging;
using Activities.Application.Queries;
using Activities.Domain.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Activities.Application.Extensions;
public static class ModuleConfiguration
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        // Register CQRS mediator
        services.AddScoped<IMediator, Mediator>();

        // Register query handlers
        services.AddTransient<IQueryHandler<GetActivitiesQuery, List<Activity>>, GetActivitiesQueryHandler>();
        services.AddTransient<IQueryHandler<GetActivityDetailsQuery, Activity>, GetActivityDetailsQueryHandler>();

        return services;
    }
}

