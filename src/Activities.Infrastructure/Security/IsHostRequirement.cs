using Activities.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Activities.Infrastructure.Security;
public class IsHostRequirement : IAuthorizationRequirement
{
}

public class IsHostRequirementHandler(ActivityContext activityContext, IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<IsHostRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return;

        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext?.GetRouteValue("id") is not string activityId) return;

        var attendee = await activityContext.ActivityAttendees.SingleOrDefaultAsync(x => x.UserId == userId && x.ActivityId == new Guid(activityId));

        if(attendee == null) return;

        if(attendee.IsHost) context.Succeed(requirement);
    }
}