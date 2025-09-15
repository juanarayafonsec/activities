using Activities.Application.Interfaces;
using Activities.Domain.Entity;
using Activities.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Activities.Infrastructure.Security;
public class UserAccessor(IHttpContextAccessor httpContextAccessor, ActivityContext context) : IUserAccessor
{
    public async Task<User> GetUserAsync()
    {
        return await context.Users.FindAsync(GetUserId())
            ?? throw new Exception("User not found");
    }

    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("User not found");
    }
}
