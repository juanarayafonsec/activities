using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class CreateActivityCommandHandler(DataContext context) : IRequestHandler<CreateActivityCommand, Result<Activity>>
{

    public async Task<Result<Activity>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        context.Activities.Add(request.Activity);
        return await context.SaveChangesAsync(cancellationToken) > 0 ?
            Result<Activity>.Success(request.Activity) : Result<Activity>.Failure("Failed to create activity");
    }
}