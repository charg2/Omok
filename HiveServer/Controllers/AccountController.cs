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
        var createResult = await _accountService.RegisterAccount( request.UserId, request.Password );

        _logger.LogInformation( $"Create result: {createResult}" );

        return new()
        {
            UserId   = request.UserId,
            Password = request.Password,
            Error    = createResult
        };
    }

    [HttpPost( "login" )]
    public async Task< LoginRes > Login( [FromBody] LoginReq request )
    {
        var ( validateResult, token )  = await _accountService.VaerifyAccount( request.UserId, request.Password );

        _logger.LogInformation( $"Login result: {validateResult}" );

        return new()
        {
            UserId   = request.UserId,
            Password = request.Password,
            Token    = token,
            Error    = validateResult
        };
    }
}
