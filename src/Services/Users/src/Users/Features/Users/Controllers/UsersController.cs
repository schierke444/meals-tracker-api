using BuildingBlocks.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Users.Features.Users.Controllers;

[Route("v1/[controller]")]
[Authorize]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
}