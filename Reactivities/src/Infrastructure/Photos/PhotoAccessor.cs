using Application.Interfaces;
using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Photos;

public class PhotoAccessor : IPhotoAccessor
{
    public Task<PhotoUploadResult> AddPhoto(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeletePhoto(IFormFile file)
    {
        throw new NotImplementedException();
    }
}