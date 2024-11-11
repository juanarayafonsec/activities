namespace Application.Commands.Photos;

public class DeletePhotoCommand : IRequest<Result<bool>>
{
    public string Id { get; set; }
}