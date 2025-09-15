using Activities.Application.Activities.DTOs;
using Activities.Application.Interfaces;
using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Activities.Application.Activities.Commands;

public record UpdateAttendanceCommand(Guid Id) : ICommand<Result<bool>>;

public sealed class UpdateAttendanceCommandHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork) : ICommandHandler<UpdateAttendanceCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(UpdateAttendanceCommand command, CancellationToken cancellationToken)
    {
        var activity = await unitOfWork.Repository<Activity>().FirstOrDefaultAsync(a => a.Id == command.Id,
            include: q => q.Include(a => a.Attendees).ThenInclude(aa => aa.User), asTracking:true, cancellationToken);

        if (activity == null) return Result<bool>.Failure("Activity not found", 404);

        var user = await userAccessor.GetUserAsync();

        var attendance = activity.Attendees.FirstOrDefault(a => a.UserId == user.Id);
        var isHost = activity.Attendees.Any(x => x.IsHost && x.UserId == user.Id);

        if (attendance != null)
        {
            if (isHost) activity.IsCancelled = !activity.IsCancelled;
            else activity.Attendees.Remove(attendance);
        }
        else
        {
            activity.Attendees.Add(new ActivityAttendee
            {
                UserId = user.Id,
                ActivityId = activity.Id,
                IsHost = false
            });
        }

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        return rows > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Problem updating the DB", 400);

    }
}
