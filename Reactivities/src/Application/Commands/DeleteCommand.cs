namespace Application.Commands;
public class DeleteCommand :IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}

