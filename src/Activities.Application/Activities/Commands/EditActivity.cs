using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;

namespace Activities.Application.Activities.Commands;

public record EditActivityCommand(Activity activity) : ICommand<Result<bool>>;

public sealed class EditActivityCommandHandler(ActivityContext context) : ICommandHandler<EditActivityCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(EditActivityCommand command, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([command.activity.Id], cancellationToken) ??
             throw new Exception("Activity not found");

        activity.Title = command.activity.Title;
        activity.Date = command.activity.Date;
        activity.Description = command.activity.Description;
        activity.Category = command.activity.Category;
        activity.City = command.activity.City;
        activity.Venue = command.activity.Venue;
        activity.Latitude = command.activity.Latitude;
        activity.Longitude = command.activity.Longitude;
        activity.IsCancelled = command.activity.IsCancelled;

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update the activity", 400);

    }
}
