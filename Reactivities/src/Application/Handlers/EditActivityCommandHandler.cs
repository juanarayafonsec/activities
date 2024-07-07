using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public EditActivityCommandHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        _context.Activities.Update(_mapper.Map<Activity>(request));
        await _context.SaveChangesAsync(cancellationToken);
    }
}

