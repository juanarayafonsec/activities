using Activities.Application.Interfaces;
using Activities.Application.Messaging;
using Activities.Domain.Entity;

namespace Activities.Application.Activities.Commands;

public record class DeleteCommand(Guid Id) : ICommand<Result<bool>>;

public sealed class DeleteCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Activity>();

        var activity = await repo.GetByIdAsync(command.Id, cancellationToken);

        if(activity is null)
        {
            return Result<bool>.Failure("Activity not found", 404);
        }

        repo.Remove(activity);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        return rows > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete the activity", 400);
    }
}

