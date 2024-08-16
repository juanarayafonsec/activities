using Application.Queries;
using Persistence;

namespace Application.Handlers;
public class GetActivityHandler : IRequestHandler<GetActivityQuery, Result<Activity>>
{
    private readonly DataContext _context;

    public GetActivityHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<Activity>> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        => Result<Activity>.Success(await _context.Activities.FindAsync(request.Id, cancellationToken));
}

