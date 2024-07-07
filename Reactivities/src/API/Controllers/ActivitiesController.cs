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
    public async Task<ActionResult<List<ActivityDto>>> GetActivities() =>
        Mapper.Map<List<ActivityDto>>(await Mediator.Send(new GetActivitiesQuery()));


    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivity(Guid id) =>
        Mapper.Map<ActivityDto>(await Mediator.Send(new GetActivityQuery { Id = id }));



    [HttpPost]
    public async Task<IActionResult> CreateActivity(CreateActivityDto activity)
    {
        var command = Mapper.Map<CreateActivityCommand>(activity);
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditActivity(ActivityDto activity)
    {
        var command = Mapper.Map<EditActivityCommand>(activity);
        await Mediator.Send(command);
        return Ok();
    }
}
