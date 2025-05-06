using FakeHiveServer.DTO.User;
using FakeHiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FakeHiveServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class RegisterController : ControllerBase
{
    private readonly ILogger< RegisterController > _logger;
    private readonly IUserService _accountService;

    public RegisterController( ILogger< RegisterController > logger, IUserService accountService )
    {
        _logger         = logger;
        _accountService = accountService;
    }

    [HttpPost]
    public async Task< RegisterRes > Post( [FromBody] RegisterReq request )
    {
        var createResult = await _accountService.RegisterUser( request.Account, request.Password );

        _logger.LogInformation( $"Create result: {createResult}" );

        return new()
        {
            Account   = request.Account,
            Password = request.Password,
            Error    = createResult
        };
    }
}
