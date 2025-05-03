using HiveServer.Model.Account;
using HiveServer.Model.Auth;
using HiveServer.Services;
using HiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HiveServer.Controllers;

[ApiController]
[Route( "api/account" )]
public class AccountController : ControllerBase
{
    private readonly ILogger< AccountController > _logger;
    private readonly IAccountService _accountService;

    public AccountController( ILogger< AccountController > logger, IAccountService accountService )
    {
        _logger         = logger;
        _accountService = accountService;
    }

    [HttpPost( "register" )]
    public async Task< RegisterRes > Register( [FromBody] RegisterReq request )
    {
        var registerResult = await _accountService.Register( request.UserId, request.Password );

        _logger.LogInformation( $"Register result: {registerResult}" );

        return new RegisterRes
        {
            UserId   = request.UserId,
            Password = request.Password,
            Token    = string.Empty,
            Error    = registerResult
        };
    }

    [HttpPost( "login" )]
    public async Task< LoginRes > Login( [FromBody] LoginReq request )
    {
        var loginResult = await _accountService.Login( request.UserId, request.Password );

        _logger.LogInformation( $"Login result: {loginResult}" );

        return new LoginRes
        {
            UserId   = request.UserId,
            Password = request.Password,
            Token    = string.Empty,
            Error    = loginResult
        };
    }
}
