using Application.Commands.Photos;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Photos.Commands;

public class DeletePhotoHandler(CoreDbContext dbContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
    : IRequestHandler<DeletePhotoCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername(), cancellationToken: cancellationToken);

        var photo = user?.Photos.FirstOrDefault(x => x.Id == request.Id);

        if (photo is null) return null;

        if (photo.IsMain) return Result<bool>.Failure("You cannot delete your main photo");

        var result = await photoAccessor.DeletePhotoAsync(photo.Id);

        if (result is null) return Result<bool>.Failure("Failed to delete photo");

        user.Photos.Remove(photo);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("Problem deleting your photo");
    }
}