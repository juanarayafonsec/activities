using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand>
{
    private readonly DataContext _context;

    public EditActivityCommandHandler(DataContext context)
    {
        _context = context;
    }
    public async Task Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        _context.Activities.Update(request.Activity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

