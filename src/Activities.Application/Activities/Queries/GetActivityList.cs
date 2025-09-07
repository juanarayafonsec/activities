using Activities.Application.Interfaces;
using Activities.Application.Messaging;
using Activities.Domain.Entity;

namespace Activities.Application.Activities.Queries;
public record GetActivitiesQuery() : IQuery<Result<List<Activity>>>;


public sealed class GetActivitiesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetActivitiesQuery, Result<List<Activity>>>
{
    public async Task<Result<List<Activity>>> HandleAsync(GetActivitiesQuery query, CancellationToken cancellationToken)
    {
        var activities = await unitOfWork.Repository<Activity>().ListAsync(cancellationToken);
        
        return Result<List<Activity>>.Success(activities.ToList());
    }
}
