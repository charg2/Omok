using FakeHiveServer.DTO.Auth;
using FakeHiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FakeHiveServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class VerifyTokenController : ControllerBase
{
    private readonly ILogger< VerifyTokenController > _logger;
    private readonly IAuthService _authService;

    public VerifyTokenController( ILogger< VerifyTokenController > logger, IAuthService authService )
    {
        _logger      = logger;
        _authService = authService;
    }

    [HttpPost]
    public async Task< VerifyTokenRes > Post( [FromBody] VerifyTokenReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );

        _logger.LogInformation( $"Verifying token for user {request.Account}" );

        return new()
        {
            Account  = request.Account,
            UserId   = userId,
            Token    = request.Token,
            Error    = verifyResult,
        };
    }

}
