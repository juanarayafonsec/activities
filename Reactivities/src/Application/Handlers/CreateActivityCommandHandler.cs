using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CreateActivityCommandHandler(DataContext context, IMapper mapper)
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

