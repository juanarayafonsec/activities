using Activities.Application.Activities.DTOs;
using Activities.Application.Interfaces;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Domain.Entity;

namespace Activities.Application.Activities.Commands;

public record EditActivityCommand(EditActivityDto activity) : ICommand<Result<bool>>;

public sealed class EditActivityCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<EditActivityCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(EditActivityCommand command, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Activity>();

        var activity = await repo.GetByIdAsync(command.activity.Id, cancellationToken) ??
             throw new Exception("Activity not found");

        activity.Map(command.activity);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        return rows > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update the activity", 400);

    }
}
