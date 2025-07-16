using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;


namespace Activities.Application.Queries;

public record GetActivityDetailsQuery(Guid Id) : IQuery<Activity>;


public class GetActivityDetailsQueryHandler(ActivityContext context) : IQueryHandler<GetActivityDetailsQuery, Activity>
{
    public async Task<Activity> HandleAsync(GetActivityDetailsQuery query, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([query.Id], cancellationToken);
        return activity == null ? throw new Exception("Activity not found") : activity;
    }
}