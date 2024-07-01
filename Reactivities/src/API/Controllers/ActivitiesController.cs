using Application.Commands;
using Application.Queries;
using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion(1)]
public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await Mediator.Send(new GetActivitiesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        return await Mediator.Send(new GetActivityQuery { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> AddActivity(Activity activity)
    {
        await Mediator.Send(new AddActivityCommand { Activity = activity });
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditActivity(Activity activity)
    {
        await Mediator.Send(new EditActivityCommand{Activity = activity});
        return Ok();
    }
}
