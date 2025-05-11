using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class InviteFriendController : ControllerBase
{
    private readonly ILogger< InviteFriendController > _logger;
    private readonly IFriendService _friendService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public InviteFriendController( ILogger< InviteFriendController > logger, IFriendService friendService, IPlayerService playerService, IAuthService authService )
    {
        _logger = logger;
        _playerService = playerService;
        _friendService = friendService;
        _authService = authService;
    }

    [HttpPost]
    public async Task< InviteFriendRes > Post( [FromBody] InviteFriendReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !verifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"Invite Friend Auth Failed: {verifyResult}" );
            return new() { Error = verifyResult };
        }

        var ( getResult, friendId ) = await _playerService.GetUserIdUsingNickName( request.FriendName );
        if ( !getResult.IsSuccess() )
        {
            _logger.LogWarning( $"Invite Friend GetUserId Failed: {getResult}" );
            return new(){ Error = getResult };
        }

        var inviteResult = await _friendService.InviteFriend( userId, friendId );
        if ( !inviteResult.IsSuccess() )
        {
            _logger.LogWarning( $"Invite Friend Failed: {inviteResult}" );
            return new(){ Error = inviteResult };
        }

        return new(){};
    }
}
