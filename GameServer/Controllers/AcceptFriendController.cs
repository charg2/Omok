using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class AcceptFriendController : ControllerBase
{
    private readonly ILogger< AcceptFriendController > _logger;
    private readonly IFriendService _friendService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public AcceptFriendController( ILogger< AcceptFriendController > logger, IFriendService friendService, IPlayerService playerService, IAuthService authService )
    {
        _logger = logger;
        _playerService = playerService;
        _friendService = friendService;
        _authService = authService;
    }

    [HttpPost]
    public async Task< AcceptFriendRes > Post( [FromBody] AcceptFriendReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !verifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"Accept Friend Auth Failed: {verifyResult}" );
            return new() { Error = verifyResult };
        }

        var ( getResult, friendId ) = await _playerService.GetUserIdUsingNickName( request.FriendName );
        if ( !getResult.IsSuccess() )
        {
            _logger.LogWarning( $"Accept Friend GetUserId Failed: {getResult}" );
            return new(){ Error = getResult };
        }

        var acceptResult = await _friendService.AcceptFriend( userId, friendId );
        if ( !acceptResult.IsSuccess() )
        {
            _logger.LogWarning( $"Accept Friend Failed: {acceptResult}" );
            return new(){ Error = acceptResult };
        }
        return new(){};
    }
}
