using Activities.Application.Activities.DTOs;
using Activities.Application.Interfaces;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Activities.Application.Activities.Queries;
public record GetActivitiesQuery() : IQuery<Result<List<ActivityDto>>>;


public sealed class GetActivitiesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetActivitiesQuery, Result<List<ActivityDto>>>
{
    public async Task<Result<List<ActivityDto>>> HandleAsync(GetActivitiesQuery query, CancellationToken cancellationToken)
    {
        var activities = await unitOfWork.Repository<Activity>().ListAsync(include: q => q
        .Include(a => a.Attendees)
        .ThenInclude(aa => aa.User), cancellationToken);
        
        return Result<List<ActivityDto>>.Success(activities.Map());
    }
}
