using Application.Dtos;

namespace Application.Queries.Activities;
public class GetActivityQuery : IRequest<Result<ActivityDto>>
{
    public Guid Id { get; set; }
}

