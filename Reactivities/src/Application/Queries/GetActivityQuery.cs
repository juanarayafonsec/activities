namespace Application.Queries;
public class GetActivityQuery : IRequest<Result<Activity>>
{
    public Guid Id { get; set; }
}

