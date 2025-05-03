using HiveServer.Model.Auth;
using HiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HiveServer.Controllers;

[ApiController]
[Route( "api/auth" )]
public class AuthController : ControllerBase
{
    private readonly ILogger< AuthController > _logger;
    private readonly IAuthService _authService;

    public AuthController( ILogger< AuthController > logger, IAuthService authService )
    {
        _logger      = logger;
        _authService = authService;
    }

    [HttpPost( "verify_token" )]
    public async Task< VerifyTokenRes > VeirifyToken( [FromBody] VerifyTokenReq request )
    {
        var verifyResult = await _authService.VerifyToken( request.UserId, request.Password, request.Token );

        _logger.LogInformation( $"Verifying token for user {request.UserId}" );

        return new()
        {
            UserId   = request.UserId,
            Password = request.Password,
            //Token    = request.Token,
        };
    }

}
