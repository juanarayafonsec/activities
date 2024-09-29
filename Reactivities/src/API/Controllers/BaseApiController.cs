using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v{apiVersion:apiVersion}/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null) return NotFound();

        if (result.IsSuccess)
            return result.Value is not null ? Ok(result.Value) : NotFound();

        return BadRequest(result.Error);
    }

}
