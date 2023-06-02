using BuildingBlocks.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Persistence;

[Route("api/v1/[controller]")]
[Authorize]
public class UsersController : BaseController
{
    public UsersController()
    {
        
    }  
}