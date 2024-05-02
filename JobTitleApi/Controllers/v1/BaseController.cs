using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IServiceProvider Resolver => HttpContext.RequestServices;

        protected T GetService<T>()
        {
            return Resolver.GetService<T>();
        }

        protected IMediator Mediator => GetService<IMediator>();

        protected ILogger Logger => GetService<ILogger>();
    }
}
