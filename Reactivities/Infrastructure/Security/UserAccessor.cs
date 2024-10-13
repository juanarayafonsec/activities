using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor contextAccessor): IUserAccessor
{
    public string GetUsername()
    {
        return contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}