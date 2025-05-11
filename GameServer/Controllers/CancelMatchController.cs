using GameServer.DTO;
using GameServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class CancelMatchController : ControllerBase
{
    private readonly ILogger< CancelMatchController > _logger;
    private readonly IMatchService _matchService;

    public CancelMatchController( ILogger< CancelMatchController > logger, IMatchService matchService, IAuthService authService )
    {
        _logger = logger;
        _matchService = matchService;
    }

    [HttpPost]
    public async Task< CancelMatchRes > Post( [FromBody] CancelMatchReq request )
    {
        throw new NotImplementedException( "CancelMatchController is not implemented yet." );
    }
}
