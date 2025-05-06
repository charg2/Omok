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
        return new();
    }
}
