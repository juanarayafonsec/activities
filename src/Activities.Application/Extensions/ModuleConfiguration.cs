using Activities.Application.Activities.Commands;
using Activities.Application.Activities.DTOs;
using Activities.Application.Activities.Queries;
using Activities.Application.Messaging;
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
        services.AddTransient<IQueryHandler<GetActivitiesQuery, Result<List<Activity>>>, GetActivitiesQueryHandler>();
        services.AddTransient<IQueryHandler<GetActivityDetailsQuery, Result<ActivityDto>>, GetActivityDetailsQueryHandler>();


        // Register command handlers
        services.AddTransient<ICommandHandler<CreateActivityCommand, Result<Guid>>, CreateActivityCommandHandler>();
        services.AddTransient<ICommandHandler<EditActivityCommand, Result<bool>>, EditActivityCommandHandler>();
        services.AddTransient<ICommandHandler<DeleteCommand, Result<bool>>, DeleteCommandHandler>();

        return services;
    }
}

