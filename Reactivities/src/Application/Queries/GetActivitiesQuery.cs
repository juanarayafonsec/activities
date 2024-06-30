using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetActivitiesQuery : IRequest<List<Activity>>;
