using Application.Commands;
using Persistence;

namespace Application.Handlers;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Result<Activity>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CreateActivityCommandHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<Activity>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        _context.Activities.Add(request.Activity);
        return await _context.SaveChangesAsync(cancellationToken) > 0 ?
            Result<Activity>.Success(request.Activity) : Result<Activity>.Failure("Failed to create activity");
    }
}

