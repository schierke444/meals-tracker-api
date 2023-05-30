﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Web;

[Route(BaseApiPath)]
[ApiController]
[ApiVersion("1.0")]
public abstract class BaseController : ControllerBase
{
    protected const string BaseApiPath = "api/v{version:apiVersion}";
}

