using Application.Commands.Photos;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Photos;

public class SetPhotoAsMainHandler(CoreDbContext dbContext, IUserAccessor userAccessor)
    : IRequestHandler<SetPhotoAsMainCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SetPhotoAsMainCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername(), cancellationToken);

        var photo = user?.Photos.FirstOrDefault(p => p.Id == request.Id);

        if (photo is null) return null;

        var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);

        if (currentMain is not null) currentMain.IsMain = false;

        photo.IsMain = true;

        return await dbContext.SaveChangesAsync(cancellationToken) > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("Failed to set photo as main");
    }
}