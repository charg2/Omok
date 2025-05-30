using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
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
                Error = verifyErrorCode,
            };
        }

        // Load Player Data
        var ( loadErrorCode, playerModel ) = await _playerService.LoadPlayer( userId );
        if ( !loadErrorCode.IsSuccess() )
        {
            _logger.LogWarning( $"User data not found for user {req.Account}" );
            return new()
            {
                Account = req.Account,
                Token = req.Token,
                Error = loadErrorCode,
            };
        }

        /// 캐싱
        var cacheResult = await _playerService.CachePlayer( userId, playerModel.NickName, req.Token );
        if ( !cacheResult.IsSuccess() )
        {
            _logger.LogWarning( $"User data cache failed for user {req.Account}: {cacheResult}" );
            return new()
            {
                Account = req.Account,
                Token = req.Token,
                Error = cacheResult,
            };
        }

        /// 플레이어 데이터가 없어도 전송, CreatePlayer 호출 유도
        return new()
        {
            Account = req.Account,
            Token = req.Token,
            Error = ErrorCode.None,
            Player = playerModel,
        };
    }

}
