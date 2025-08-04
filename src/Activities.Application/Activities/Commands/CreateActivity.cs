using Activities.Application.Activities.DTOs;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Infrastructure.Persistance.Context;

namespace Activities.Application.Activities.Commands;

public record CreateActivityCommand(CreateActivityDto activity) : ICommand<Guid>;

public sealed class CreateActivityCommandHandler(ActivityContext context) : ICommandHandler<CreateActivityCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        var activity = command.activity.Map();

        context.Activities.Add(activity);

        await context.SaveChangesAsync(cancellationToken);

        return activity.Id;
    }
}