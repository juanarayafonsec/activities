using Application.Dtos;
using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Application.Mappings;

namespace Application.Handlers;

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