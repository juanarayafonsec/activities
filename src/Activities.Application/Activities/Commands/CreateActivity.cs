using Activities.Application.Activities.DTOs;
using Activities.Application.Interfaces;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Domain.Entity;

namespace Activities.Application.Activities.Commands;

public record CreateActivityCommand(CreateActivityDto activity) : ICommand<Result<Guid>>;

public sealed class CreateActivityCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : ICommandHandler<CreateActivityCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var activity = command.activity.Map();

        await unitOfWork.Repository<Activity>().AddAsync(activity, cancellationToken);

        await unitOfWork.Repository<ActivityAttendee>().AddAsync(new ActivityAttendee
        {
            ActivityId = activity.Id,
            UserId = user.Id,
            IsHost = true,
            DateJoined = DateTime.UtcNow
        }, cancellationToken);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        return rows > 0 ? Result<Guid>.Success(activity.Id) : Result<Guid>.Failure("Failed to create the activity", 400);
    }
}