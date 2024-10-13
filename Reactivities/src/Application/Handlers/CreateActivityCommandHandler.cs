using Application.Commands;
using Application.Dtos;
using Application.Interfaces;
using Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers;
public class CreateActivityCommandHandler(CoreDbContext context, IUserAccessor userAccessor) : IRequestHandler<CreateActivityCommand, Result<ActivityDto>>
{

    public async Task<Result<ActivityDto>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername(), cancellationToken);

            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };
            request.Activity.Attendees.Add(attendee);
        
            context.Activities.Add(request.Activity);
        
            return await context.SaveChangesAsync(cancellationToken) > 0 ?
                Result<ActivityDto>.Success(request.Activity.ToActivityDto()) : Result<ActivityDto>.Failure("Failed to create activity");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}