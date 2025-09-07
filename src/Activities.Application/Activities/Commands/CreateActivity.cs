using Activities.Application.Activities.DTOs;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Domain.Interfaces;

namespace Activities.Application.Activities.Commands;

public record CreateActivityCommand(CreateActivityDto activity) : ICommand<Result<Guid>>;

public sealed class CreateActivityCommandHandler(IActivityRepository activityRepo) : ICommandHandler<CreateActivityCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        var activity = command.activity.Map();

        var result = await activityRepo.AddAsync(activity, cancellationToken);

        return result > 0 ? Result<Guid>.Success(activity.Id) : Result<Guid>.Failure("Failed to create the activity", 400);
    }
}