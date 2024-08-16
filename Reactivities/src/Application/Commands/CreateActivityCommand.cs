namespace Application.Commands;

public class CreateActivityCommand : IRequest<Result<Activity>>
{
    public Activity Activity { get; set; }
}
