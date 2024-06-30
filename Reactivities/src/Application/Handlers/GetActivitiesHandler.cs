using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers;
public class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, List<Activity>>
{
    private readonly DataContext _context;

    public GetActivitiesHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    => await _context.Activities.ToListAsync(cancellationToken: cancellationToken);

}
