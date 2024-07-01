namespace Application.Commands;
public class EditActivityCommand : IRequest
{
    public Activity Activity { get; set; }
}