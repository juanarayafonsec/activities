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
    public async Task<IActionResult> GetActivities(CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new GetActivitiesQuery(), cancellationToken));


    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new GetActivityQuery { Id = id }, cancellationToken));



    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new CreateActivityCommand { Activity = activity }, cancellationToken));


    [HttpPut]
    public async Task<IActionResult> EditActivity(Activity activity, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new EditActivityCommand { Activity = activity }, cancellationToken));



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new DeleteCommand { Id = id }, cancellationToken));

}