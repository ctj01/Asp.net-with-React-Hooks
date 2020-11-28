using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiRest.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        private IMediator _Imediator;
        protected IMediator Mediador => _Imediator ?? (_Imediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
