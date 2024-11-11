namespace Application.Commands.Photos;

public class SetPhotoAsMainCommand : IRequest<Result<bool>>
{
    public string Id { get; set; }
}