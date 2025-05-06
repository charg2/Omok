using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReadFriendListController : ControllerBase
{
    private readonly ILogger< ReadFriendListController > _logger;
    private readonly IFriendService _FriendService;
    private readonly IAuthService _authService;

    public ReadFriendListController( ILogger< ReadFriendListController > logger, IFriendService FriendService, IAuthService authService )
    {
        _logger = logger;
        _authService = authService;
        _FriendService = FriendService;
    }

    [HttpPost]
    public async Task< ReadFriendListRes > Post( [FromBody] ReadFriendListReq request )
    {
        return new();
        //var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        //if ( !verifyResult.IsSuccess() )
        //{
        //    _logger.LogWarning( $"SendFriend failed: {verifyResult}" );
        //    return new(){ Error = verifyResult };
        //}

        //var ( readResult, FriendList ) = await _FriendService.ReadFriendList( userId, request.LastReadFriendId );
        //return new()
        //{
        //    Error = readResult,
        //    FriendList = FriendList
        //};
    }
}
