using Activities.Application.Activities.DTOs;
using Activities.Application.Interfaces;
using Activities.Application.Mappings;
using Activities.Application.Messaging;
using Activities.Application.Profiles.DTOs;
using Activities.Domain.Entity;
using Microsoft.EntityFrameworkCore;


namespace Activities.Application.Activities.Queries;

public record GetActivityDetailsQuery(Guid Id) : IQuery<Result<ActivityDto>>;


public class GetActivityDetailsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetActivityDetailsQuery, Result<ActivityDto>>
{
    public async Task<Result<ActivityDto>> HandleAsync(GetActivityDetailsQuery query, CancellationToken cancellationToken)
    {
        var activity = await unitOfWork.Repository<Activity>().FirstOrDefaultAsync(a => a.Id == query.Id,
            include: q => q.Include(a => a.Attendees).ThenInclude(aa => aa.User), cancellationToken);

        if (activity == null) return Result<ActivityDto>.Failure("Activity not found", 404);

        return Result<ActivityDto>.Success(activity.Map());
    }
}