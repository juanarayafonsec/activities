using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class AddActivityHandler : IRequestHandler<AddActivityCommand>
{
    private readonly DataContext _context;
    public AddActivityHandler(DataContext context)
    {
        _context = context;
    }
    public async Task Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
         _context.Activities.Add(request.Activity);
         await _context.SaveChangesAsync(cancellationToken);
    }
}

