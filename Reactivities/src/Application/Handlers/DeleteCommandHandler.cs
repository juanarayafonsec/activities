using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<bool>>
{
    private readonly DataContext _context;

    public DeleteCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities.FindAsync(request.Id,cancellationToken);

        if (activity is null) return null;

        _context.Remove(activity);

        return await _context.SaveChangesAsync(cancellationToken) > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete the activity");
    }
}

