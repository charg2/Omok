using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class AddFriendController : ControllerBase
{
    private readonly ILogger< AddFriendController > _logger;
    private readonly IFriendService _friendService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public AddFriendController( ILogger< AddFriendController > logger, IFriendService friendService, IPlayerService playerService, IAuthService authService )
    {
        _logger = logger;
        _playerService = playerService;
        _friendService = friendService;
        _authService = authService;
    }

    [HttpPost]
    public async Task< AddFriendRes > Post( [FromBody] AddFriendReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !verifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"Add Friend Auth Failed: {verifyResult}" );
            return new() { Error = verifyResult };
        }

        var ( getResult, friendId ) = await _playerService.GetUserIdUsingNickName( request.FriendName );
        if ( !getResult.IsSuccess() )
        {
            _logger.LogWarning( $"Add Friend GetUserId Failed: {getResult}" );
            return new(){ Error = getResult };
        }

        var addResult = await _friendService.AddFriend( userId, friendId );
        return new(){};
    }
}
