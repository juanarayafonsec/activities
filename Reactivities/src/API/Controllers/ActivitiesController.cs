using Application.Commands;
using Application.Dtos;
using Application.Queries;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion(1)]
public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetActivities(CancellationToken cancellationToken) =>
        Mapper.Map<List<ActivityDto>>(await Mediator.Send(new GetActivitiesQuery(), cancellationToken));


    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivity(Guid id, CancellationToken cancellationToken) =>
        Mapper.Map<ActivityDto>(await Mediator.Send(new GetActivityQuery { Id = id }, cancellationToken));



    [HttpPost]
    public async Task<IActionResult> CreateActivity(CreateActivityDto activity, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateActivityCommand>(activity);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditActivity(ActivityDto activity, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<EditActivityCommand>(activity);
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteCommand { Id = id }, cancellationToken);
        return Ok();
    }
}
