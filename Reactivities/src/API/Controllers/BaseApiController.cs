using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v{apiVersion:apiVersion}/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    private IMapper _mapper;

    protected IMediator Mediator => _mediator??= HttpContext.RequestServices.GetService<IMediator>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

}
