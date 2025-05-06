using GameServer.DTO;
using GameServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class RemoveFriendController : ControllerBase
{
    private readonly ILogger< RemoveFriendController > _logger;
    private readonly IFriendService _friendService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public RemoveFriendController( ILogger< RemoveFriendController > logger, IFriendService friendService, IPlayerService playerService, IAuthService authService )
    {
        _logger = logger;
        _playerService = playerService;
        _friendService = friendService;
        _authService = authService;
    }

    [HttpPost]
    public async Task< RemoveFriendRes > Post( [FromBody] RemoveFriendReq request )
    {
        return new();
    }
}
