using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
{
    private readonly DataContext _context;

    public DeleteCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities.FindAsync(request.Id);
        _context.Remove(activity);
        await _context.SaveChangesAsync();
    }
}

