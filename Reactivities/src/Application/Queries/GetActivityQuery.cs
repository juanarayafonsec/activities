using Application.Dtos;

namespace Application.Queries;
public class GetActivityQuery : IRequest<Result<ActivityDto>>
{
    public Guid Id { get; set; }
}

