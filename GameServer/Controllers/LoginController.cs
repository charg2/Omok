using GameServer.Models.User;
using GameServer.Repository;
using GameServer.Services.Interface;
using HiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class LoginController : ControllerBase
{
    private readonly ILogger< LoginController > _logger;
    private readonly IAuthService _authService;
    private readonly IPlayerService _playerService;

    public LoginController( ILogger< LoginController > logger, IAuthService authService, IPlayerService userService )
    {
        _logger = logger;
        _authService = authService;
        _playerService = userService;
    }

    [HttpPost]
    public async Task< GameLoginRes > Post( [FromBody] GameLoginReq req )
    {
        // Validate the request
        var ( verifyErrorCode, userId ) = await _authService.VerifyToken( req.Account, req.Token );
        if ( !verifyErrorCode.IsSuccess() )
        {
            _logger.LogWarning( $"Login failed for user {req.Account}: {verifyErrorCode}" );
            return new()
            {
                Account = req.Account,
                Token = req.Token,
                Error = ErrorCode.None,
            };
        }

        // Load Player Data
        var ( loadErrorCode, playerData ) = await _playerService.LoadPlayer( userId );
        if ( !loadErrorCode.IsSuccess() )
        {
            _logger.LogWarning( $"User data not found for user {req.Account}" );
            return new()
            {
                Account = req.Account,
                Token = req.Token,
                Error = ErrorCode.None,
            };
        }

        /// 플레이어 데이터가 없어도 전송, CreatePlayer 호출 유도
        return new()
        {
            Account = req.Account,
            Token = req.Token,
            Error = ErrorCode.None,
        };
    }

}
