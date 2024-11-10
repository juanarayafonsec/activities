using Microsoft.AspNetCore.Http;

namespace Application.Commands.Photos;

public class AddPhotoCommand : IRequest<Result<Photo>>
{
    public IFormFile File { get; set; }
}