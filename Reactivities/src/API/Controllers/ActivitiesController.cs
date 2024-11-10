using Application.Commands;
using Application.Commands.Activities;
using Application.Commands.Attendees;
using Application.Dtos;
using Application.Handlers;
using Application.Mappings;
using Application.Queries;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> CreateActivity(CreateActivityDto activity, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new CreateActivityCommand { Activity = activity.Map() }, cancellationToken));

    [Authorize(Policy = "IsActivityHost")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, EditActivityDto editActivity, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new EditActivityCommand { Activity = editActivity.Map(id) }, cancellationToken));

    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new DeleteCommand { Id = id }, cancellationToken));

    [HttpPost("{id}/attend")]
    public async Task<IActionResult> Attend(Guid id, CancellationToken cancellationToken) =>
        HandleResult(await Mediator.Send(new UpdateAttendanceCommand { Id = id }, cancellationToken));
}