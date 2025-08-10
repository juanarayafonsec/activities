using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Activities.Application.Activities.Queries;
public record GetActivitiesQuery() : IQuery<Result<List<Activity>>>;


public sealed class GetActivitiesQueryHandler(ActivityContext context) : IQueryHandler<GetActivitiesQuery, Result<List<Activity>>>
{
    public async Task<Result<List<Activity>>> HandleAsync(GetActivitiesQuery query, CancellationToken cancellationToken)
    {
        var activities = await context.Activities.ToListAsync(cancellationToken);
        
        return Result<List<Activity>>.Success(activities);
    }
}
