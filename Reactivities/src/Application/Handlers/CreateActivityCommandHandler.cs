using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Activity>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CreateActivityCommandHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Activity> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var newEntity = _mapper.Map<Activity>(request);
        _context.Activities.Add(newEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return newEntity;
    }
}

