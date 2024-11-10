namespace Application.Commands.Activities;
public class EditActivityCommand : IRequest<Result<bool>>
{
    public Activity Activity { get; set; }
}