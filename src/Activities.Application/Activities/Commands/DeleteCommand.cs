using Activities.Application.Messaging;
using Activities.Infrastructure.Persistance.Context;

namespace Activities.Application.Activities.Commands;

public record class DeleteCommand(Guid Id) : ICommand<Result<bool>>;

public sealed class DeleteCommandHandler(ActivityContext context) : ICommandHandler<DeleteCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([command.Id], cancellationToken);

        if(activity is null)
        {
            return Result<bool>.Failure("Activity not found", 404);
        }   

        context.Activities.Remove(activity);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete the activity", 400);
    }
}

