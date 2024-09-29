using Application.Queries;
using Persistence.Context;

namespace Application.Handlers;
public class GetActivityHandler(DataContext context) : IRequestHandler<GetActivityQuery, Result<Activity>>
{
    public async Task<Result<Activity>> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        => Result<Activity>.Success(await context.Activities.FindAsync(request.Id, cancellationToken));
}

