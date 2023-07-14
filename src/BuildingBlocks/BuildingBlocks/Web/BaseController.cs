using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Web;

// [Route(BaseApiPath)]
[ApiController]
[ApiVersion("1.0")]
public abstract class BaseController : ControllerBase
{
    // protected const string BaseApiPath = "[controller]";
    protected readonly IMediator mediator;
    public BaseController(IMediator mediator)
    {
        this.mediator = mediator;
    }
}

