using Application.Dtos;

namespace Application.Commands;

public class CreateActivityCommand : IRequest<Result<ActivityDto>>
{
    public Activity Activity { get; set; }
}
