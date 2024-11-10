namespace Application.Commands.Activities;
public class DeleteCommand :IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}

