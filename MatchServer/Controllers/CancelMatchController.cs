using MatchServer.DTO;
using MatchServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MatchServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class CancelMatchController : ControllerBase
{
    private readonly ILogger< CancelMatchController > _logger;
    private readonly IMatchService _matchService;

    public CancelMatchController( ILogger< CancelMatchController > logger, IMatchService matchService )
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
