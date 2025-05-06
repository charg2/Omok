using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class CreatePlayerController : ControllerBase
{
    private readonly ILogger< CreatePlayerController > _logger;
    private readonly IAuthService _authService;
    private readonly IPlayerService _playerService;

    public CreatePlayerController( ILogger< CreatePlayerController > logger, IAuthService authService, IPlayerService userService )
    {
        _logger = logger;
        _playerService = userService;
        _authService = authService;
    }

    [HttpPost]
    public async Task< CreatePlayerRes > Post( [FromBody] CreatePlayerReq request )
    {
        var ( tokenVerifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !tokenVerifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"Create player failed for user {request.Account}: {tokenVerifyResult}" );
            return new(){ Error = tokenVerifyResult };
        }

        if ( userId != request.UserId )
        {
            _logger.LogWarning( $"Create player failed for user {request.Account}: UserId mismatch" );
            return new(){ Error = ErrorCode.InvalidUserId };
        }

        var createResult = await _playerService.CreatePlayer( request.UserId, request.NickName );
        if ( !createResult.IsSuccess() )
        {
            _logger.LogWarning( $"Create player failed for user {request.Account}: {createResult}" );
            return new() { Error = createResult };
        }

        return new()
        {
            UserId   = request.UserId,
            NickName = request.NickName,
            Error    = ErrorCode.None,
        };
    }

}
