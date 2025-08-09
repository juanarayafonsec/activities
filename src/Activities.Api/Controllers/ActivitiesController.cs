using Activities.Application.Activities.Commands;
using Activities.Application.Activities.DTOs;
using Activities.Application.Activities.Queries;
using Activities.Application.Messaging;
using Activities.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Activities.Api.Controllers;
public class ActivitiesController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        var query = new GetActivitiesQuery();
        return await mediator.SendQueryAsync<GetActivitiesQuery, List<Activity>>(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(string id)
     {
        var query = new GetActivityDetailsQuery(new Guid(id));
        
        var ressult = await mediator.SendQueryAsync<GetActivityDetailsQuery, Result<Activity>>(query);

        return HandleResult(ressult);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateActivity(CreateActivityDto activity)
    {
        var command = new CreateActivityCommand(activity);

        return await mediator.SendCommandAsync<CreateActivityCommand, Guid>(command);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateActivity(Activity activity)
    {
        var command = new EditActivityCommand(activity);

        await mediator.SendCommandAsync<EditActivityCommand, bool>(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        var command = new DeleteCommand(new Guid(id));

        await mediator.SendCommandAsync<DeleteCommand, bool>(command);

        return Ok();
    }
}

