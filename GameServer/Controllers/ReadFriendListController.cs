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
    private readonly IFriendService _friendService;
    private readonly IAuthService _authService;

    public ReadFriendListController( ILogger< ReadFriendListController > logger, IFriendService FriendService, IAuthService authService )
    {
        _logger = logger;
        _authService = authService;
        _friendService = FriendService;
    }

    [HttpPost]
    public async Task< ReadFriendListRes > Post( [FromBody] ReadFriendListReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !verifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"Read Friend failed: {verifyResult}" );
            return new() { Error = verifyResult };
        }

        var ( readResult, friendList ) = await _friendService.ReadFriendList( userId );
        return new()
        {
            Error = readResult,
            FriendList = friendList
        };
    }
}
