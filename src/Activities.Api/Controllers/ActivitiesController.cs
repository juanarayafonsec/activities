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
        var activities = await mediator.SendQueryAsync<GetActivitiesQuery, Result<List<Activity>>>(query);
        return HandleResult(activities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivity(string id)
     {
        var query = new GetActivityDetailsQuery(new Guid(id));
        
        var ressult = await mediator.SendQueryAsync<GetActivityDetailsQuery, Result<ActivityDto>>(query);

        return HandleResult(ressult);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateActivity(CreateActivityDto activity)
    {
        var command = new CreateActivityCommand(activity);

        var result = await mediator.SendCommandAsync<CreateActivityCommand, Result<Guid>>(command);

        return HandleResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateActivity(EditActivityDto activity)
    {
        var command = new EditActivityCommand(activity);

        var result = await mediator.SendCommandAsync<EditActivityCommand, Result<bool>>(command);

        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteActivity(string id)
    {
        var command = new DeleteCommand(new Guid(id));
        
        var result = await mediator.SendCommandAsync<DeleteCommand, Result<bool>>(command);
        
        return HandleResult(result);
    }
}

