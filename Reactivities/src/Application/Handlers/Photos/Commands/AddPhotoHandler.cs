using Application.Commands.Photos;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Handlers.Photos.Commands;

public class AddPhotoHandler(CoreDbContext dbContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
    : IRequestHandler<AddPhotoCommand, Result<Photo>>
{
    public async Task<Result<Photo>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.UserName == userAccessor.GetUsername(), cancellationToken: cancellationToken);

        if (user is null) return null;

        var photoUploadResult = await photoAccessor.AddPhotoAsync(request.File);

        var photo = new Photo
        {
            Url = photoUploadResult.Url,
            Id = photoUploadResult.PublicId
        };

        if (!user.Photos.Any(x => x.IsMain))
            photo.IsMain = true;

        user.Photos.Add(photo);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0
            ? Result<Photo>.Success(photo)
            : Result<Photo>.Failure("Problem adding Photo");
    }
}