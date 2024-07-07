using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class CreateActivityHandler : IRequestHandler<CreateActivityCommand>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CreateActivityHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
         _context.Activities.Add(_mapper.Map<Activity>(request));
         await _context.SaveChangesAsync(cancellationToken);
    }
}

