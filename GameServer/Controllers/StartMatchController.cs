using GameServer.DTO;
using GameServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class StartMatchController : ControllerBase
{
    private readonly ILogger< StartMatchController > _logger;
    private readonly IMatchService _matchService;

    public StartMatchController( ILogger< StartMatchController > logger, IMatchService matchService, IAuthService authService )
    {
        _logger = logger;
        _matchService = matchService;
    }

    [HttpPost]
    public async Task< StartMatchRes > Post( [FromBody] StartMatchReq request )
    {
        throw new NotImplementedException( "StartMatchController is not implemented yet." );
    }
}
