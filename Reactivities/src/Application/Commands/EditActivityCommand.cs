namespace Application.Commands;
public class EditActivityCommand : IRequest<Result<bool>>
{
    public Activity Activity { get; set; }
}