using Application.Dtos;
using Application.Mappings;
using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Activities;

public class GetActivityHandler(CoreDbContext context) : IRequestHandler<GetActivityQuery, Result<ActivityDto>>
{
    public async Task<Result<ActivityDto>> Handle(GetActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.Where(a => a.Id == request.Id)
            .Include(a => a.Attendees)
            .ThenInclude(att => att.AppUser)
            .FirstOrDefaultAsync(cancellationToken);

        return Result<ActivityDto>.Success(activity.ToActivityDto());
    }
}