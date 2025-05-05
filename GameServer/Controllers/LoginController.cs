using GameServer.Models.User;
using GameServer.Repository;
using GameServer.Services.Interface;
using HiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger< LoginController > _logger;
    private readonly IUserService _userService;

    public LoginController( ILogger< LoginController > logger, IUserService userService )
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task< GameLoginRes > Post( [FromBody] GameLoginReq request )
    {
        var ( errorCode, userData ) = await _userService.Login( request.Account, request.Token );
        if ( errorCode != ErrorCode.None )
        {
            _logger.LogWarning( $"Login failed for user {request.Account}: {errorCode}" );
            return PostResponse( request, errorCode, userData );
        }

        return PostResponse( request, ErrorCode.None, userData );
    }

    public GameLoginRes PostResponse( GameLoginReq req, ErrorCode errorCode, UserLoadData userData )
    {
        return new()
        {
            Account = req.Account,
            Token = req.Token,
            Error = errorCode,
        };
    }

}
