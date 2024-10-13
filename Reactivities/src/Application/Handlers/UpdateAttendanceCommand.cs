namespace Application.Handlers;

public class UpdateAttendanceCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}