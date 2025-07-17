using Activities.Application.Messaging;
using Activities.Infrastructure.Persistance.Context;

namespace Activities.Application.Activities.Commands;

public record class DeleteCommand(Guid Id) : ICommand<bool>;

public sealed class DeleteCommandHandler(ActivityContext context) : ICommandHandler<DeleteCommand, bool>
{
    public async Task<bool> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([command.Id], cancellationToken) ??
                       throw new Exception("Activity not found");
        context.Activities.Remove(activity);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}

