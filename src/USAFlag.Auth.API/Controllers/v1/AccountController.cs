using USAFlag.Auth.Contracts.Requests;
using USAFlag.Auth.Core.Application.Handlers.Commands;

namespace USAFlag.Auth.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class AccountController : BaseApiController
{
    private readonly IConfiguration config;
    private readonly IMediator mediator;

    public AccountController(IConfiguration config, IMediator mediator) : base(config)
    {
        this.config = config;
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        //ResolveUserIdentity();
        var result = await mediator.Send(new GenericCommand<CreateUserRequest>(request, 123));
        return Ok(result);
    }

}
