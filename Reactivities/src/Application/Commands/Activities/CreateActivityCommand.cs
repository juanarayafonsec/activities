using Application.Dtos;

namespace Application.Commands.Activities;

public class CreateActivityCommand : IRequest<Result<ActivityDto>>
{
    public Activity Activity { get; set; }
}
