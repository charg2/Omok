using FakeHiveServer.DTO.Auth;
using FakeHiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FakeHiveServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class RefreshTokenController : ControllerBase
{
    private readonly ILogger< RefreshTokenController > _logger;
    private readonly IAuthService _authService;

    public RefreshTokenController( ILogger< RefreshTokenController > logger, IAuthService authService )
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost]
    public async Task< VerifyTokenRes > Post( [FromBody] VerifyTokenReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        return new()
        {
            Account = request.Account,
            Token = request.Token,
            UserId = userId,
            Error = verifyResult,
        };
    }
}
