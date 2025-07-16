using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Activities.Application.Queries;
public sealed class GetActivitiesQuery() : IQuery<List<Activity>>;


public sealed class GetActivitiesQueryHandler(ActivityContext context) : IQueryHandler<GetActivitiesQuery, List<Activity>>
{
    public async Task<List<Activity>> HandleAsync(GetActivitiesQuery query, CancellationToken cancellationToken)
    {
        return await context.Activities.ToListAsync(cancellationToken);
    }
}
