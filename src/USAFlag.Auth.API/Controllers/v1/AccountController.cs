namespace USAFlag.Auth.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
//[Authorize(AuthenticationSchemes = "Bearer")]
public class AccountController(IConfiguration config, IMediator mediator) : BaseApiController(config)
{

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        //ResolveUserIdentity();
        var response = await mediator.Send(new GenericCommand<CreateUserRequest>(request, 123));
        return new ObjectResult(response);
    }

}
