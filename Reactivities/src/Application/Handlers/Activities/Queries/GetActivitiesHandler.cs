using Application.Dtos;
using Application.Mappings;
using Application.Queries;
using Application.Queries.Activities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Activities.Queries;

public class GetActivitiesHandler(CoreDbContext context) : IRequestHandler<GetActivitiesQuery, Result<List<ActivityDto>>>
{
    public async Task<Result<List<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = await context.Activities
            .Include(a => a.Attendees)
            .ThenInclude(u => u.AppUser)
            .ThenInclude(p => p.Photos)
            .ToListAsync(cancellationToken);
        
        return Result<List<ActivityDto>>.Success(activities.ToActivityDto());
    }
}