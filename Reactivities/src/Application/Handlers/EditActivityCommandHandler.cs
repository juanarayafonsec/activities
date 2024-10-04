using Application.Commands;
using Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers;

public class EditActivityCommandHandler(DataContext context) : IRequestHandler<EditActivityCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        var activity =
            await context.Activities.FirstOrDefaultAsync(x => x.Id == request.Activity.Id, cancellationToken);

        if (activity is null) return null;

        activity.Map(request.Activity);
        
        return await context.SaveChangesAsync(cancellationToken) > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("Failed to update activity");
    }
}