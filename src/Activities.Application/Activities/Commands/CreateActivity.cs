using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;

namespace Activities.Application.Activities.Commands;

public record CreateActivityCommand(Activity activity) : ICommand<Guid>;

public sealed class CreateActivityCommandHandler(ActivityContext context) : ICommandHandler<CreateActivityCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        
        context.Activities.Add(command.activity);

        await context.SaveChangesAsync(cancellationToken);

        return command.activity.Id;
    }
}