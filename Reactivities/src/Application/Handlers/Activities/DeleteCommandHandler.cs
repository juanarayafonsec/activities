using Application.Commands.Activities;
using Persistence.Context;

namespace Application.Handlers.Activities;
public class DeleteCommandHandler(CoreDbContext context) : IRequestHandler<DeleteCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync(request.Id,cancellationToken);

        if (activity is null) return null;

        context.Remove(activity);

        return await context.SaveChangesAsync(cancellationToken) > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete the activity");
    }
}

