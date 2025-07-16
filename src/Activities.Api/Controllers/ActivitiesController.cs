using Activities.Application.Messaging;
using Activities.Application.Queries;
using Activities.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Activities.Api.Controllers;
public class ActivitiesController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken cancellationToken)
    {
        //return await mediator.Send(new GetActivityList.Query());
        var query = new GetActivitiesQuery();
        return await mediator.SendQueryAsync<GetActivitiesQuery, List<Activity>>(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(string id)
    {
        var query = new GetActivityDetailsQuery(new Guid(id));

        return await mediator.SendQueryAsync<GetActivityDetailsQuery,Activity>(query);
    }
}

