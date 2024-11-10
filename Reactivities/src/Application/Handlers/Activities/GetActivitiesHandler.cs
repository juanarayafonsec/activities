using Application.Dtos;
using Application.Mappings;
using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Activities;

public class GetActivitiesHandler(CoreDbContext context) : IRequestHandler<GetActivitiesQuery, Result<List<ActivityDto>>>
{
    public async Task<Result<List<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = await context.Activities
            .Include(a => a.Attendees)
            .ThenInclude(p => p.AppUser).ToListAsync(cancellationToken);
        
        return Result<List<ActivityDto>>.Success(activities.ToActivityDto());
    }
}