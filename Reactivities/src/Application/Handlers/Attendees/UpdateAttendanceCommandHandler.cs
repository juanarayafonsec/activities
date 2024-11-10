using Application.Commands.Attendees;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Attendees;

public class UpdateAttendanceCommandHandler(CoreDbContext context, IUserAccessor accessor)
    : IRequestHandler<UpdateAttendanceCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities
            .Include(a => a.Attendees).ThenInclude(u => u.AppUser).SingleOrDefaultAsync(
                x => x.Id == request.Id, cancellationToken);
        if (activity == null) return null;

        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == accessor.GetUsername(), cancellationToken);

        if (user == null) return null;

        var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

        var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);
       
        if (attendance != null && hostUsername == user.UserName)
            activity.IsCancelled = !activity.IsCancelled;

        if (attendance != null && hostUsername != user.UserName)
            activity.Attendees.Remove(attendance);
        if (attendance == null)
        {
            attendance = new ActivityAttendee
            {
                AppUser = user,
                Activity = activity,
                IsHost = false
            };
            activity.Attendees.Add(attendance);
        }

        return await context.SaveChangesAsync(cancellationToken) > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("Problem updating attendance");
    }
}