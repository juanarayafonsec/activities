namespace Application.Commands;
public class DeleteCommand :IRequest
{
    public Guid Id { get; set; }
}

