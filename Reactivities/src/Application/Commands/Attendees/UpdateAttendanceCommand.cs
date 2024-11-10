namespace Application.Commands.Attendees;

public class UpdateAttendanceCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}