using Application.Dtos;

namespace Application.Queries.Activities;

public class GetActivitiesQuery : IRequest<Result<List<ActivityDto>>>;
