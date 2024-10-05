using Application.Commands;
using Persistence;
using Persistence.Context;

namespace Application.Handlers;
public class CreateActivityCommandHandler(CoreDbContext contextt) : IRequestHandler<CreateActivityCommand, Result<Activity>>
{

    public async Task<Result<Activity>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        contextt.Activities.Add(request.Activity);
        return await contextt.SaveChangesAsync(cancellationToken) > 0 ?
            Result<Activity>.Success(request.Activity) : Result<Activity>.Failure("Failed to create activity");
    }
}