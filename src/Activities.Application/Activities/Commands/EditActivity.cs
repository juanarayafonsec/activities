using Activities.Application.Activities.DTOs;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Infrastructure.Persistance.Context;

namespace Activities.Application.Activities.Commands;

public record EditActivityCommand(EditActivityDto activity) : ICommand<Result<bool>>;

public sealed class EditActivityCommandHandler(ActivityContext context) : ICommandHandler<EditActivityCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(EditActivityCommand command, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([command.activity.Id], cancellationToken) ??
             throw new Exception("Activity not found");

        activity.Map(command.activity);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update the activity", 400);

    }
}
