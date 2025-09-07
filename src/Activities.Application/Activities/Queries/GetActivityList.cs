using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Domain.Interfaces;

namespace Activities.Application.Activities.Queries;
public record GetActivitiesQuery() : IQuery<Result<List<Activity>>>;


public sealed class GetActivitiesQueryHandler(IActivityRepository activityRepo) : IQueryHandler<GetActivitiesQuery, Result<List<Activity>>>
{
    public async Task<Result<List<Activity>>> HandleAsync(GetActivitiesQuery query, CancellationToken cancellationToken)
    {
        var activities = await activityRepo.ListAsync(cancellationToken);
        
        return Result<List<Activity>>.Success(activities);
    }
}
