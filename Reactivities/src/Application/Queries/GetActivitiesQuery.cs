using Application.Dtos;

namespace Application.Queries;

public class GetActivitiesQuery : IRequest<Result<List<ActivityDto>>>;
