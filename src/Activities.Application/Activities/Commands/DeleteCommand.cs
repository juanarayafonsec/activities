using Activities.Application.Messaging;
using Activities.Domain.Interfaces;

namespace Activities.Application.Activities.Commands;

public record class DeleteCommand(Guid Id) : ICommand<Result<bool>>;

public sealed class DeleteCommandHandler(IActivityRepository activityRepo) : ICommandHandler<DeleteCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var activity = await activityRepo.GetByIdAsync(command.Id, cancellationToken);

        if(activity is null)
        {
            return Result<bool>.Failure("Activity not found", 404);
        }

        var result = await activityRepo.DeleteAsync(activity, cancellationToken);

        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete the activity", 400);
    }
}

