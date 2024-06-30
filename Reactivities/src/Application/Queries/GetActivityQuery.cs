using Domain.Entities;
using MediatR;

namespace Application.Queries;
public class GetActivityQuery : IRequest<Activity>
{
    public Guid Id { get; set; }
}

