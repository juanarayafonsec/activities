using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v{apiVersion:apiVersion}/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
}
