using Application.Queries;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Handlers;
public class GetActivityHandler : IRequestHandler<GetActivityQuery, Activity>
{
    private readonly DataContext _context;

    public GetActivityHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Activity> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        => await _context.Activities.FindAsync(request.Id);
}

