using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;


namespace Activities.Application.Activities.Queries;

public record GetActivityDetailsQuery(Guid Id) : IQuery<Result<Activity>>;


public class GetActivityDetailsQueryHandler(ActivityContext context) : IQueryHandler<GetActivityDetailsQuery, Result<Activity>>
{
    public async Task<Result<Activity>> HandleAsync(GetActivityDetailsQuery query, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([query.Id], cancellationToken);
        
        if (activity == null) return Result<Activity>.Failure("Activity not found", 404);

        return Result<Activity>.Success(activity);
    }
}