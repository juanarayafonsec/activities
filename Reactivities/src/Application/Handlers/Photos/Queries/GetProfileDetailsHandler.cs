using Application.Mappings;
using Application.Profiles;
using Application.Queries.Profiles;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Photos.Queries;

public class GetProfileDetailsHandler(CoreDbContext dbContext)
    : IRequestHandler<GetProfileDetailsQuery, Result<Profile>>
{
    public async Task<Result<Profile>> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.Include(p => p.Photos)
            .SingleOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

        return user is null ? null : Result<Profile>.Success(user.Map());
    }
}