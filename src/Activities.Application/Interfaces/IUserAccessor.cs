using Activities.Domain.Entity;

namespace Activities.Application.Interfaces;
public interface IUserAccessor
{
    string GetUserId();
    Task<User> GetUserAsync();
}

