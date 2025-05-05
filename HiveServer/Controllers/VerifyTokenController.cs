using HiveServer.Models.Auth;
using HiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HiveServer.Controllers;

[ApiController]
[Route( "api/[contoller]" )]
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
        var verifyResult = await _authService.VerifyToken( request.Account, request.Token );

        _logger.LogInformation( $"Verifying token for user {request.Account}" );

        return new()
        {
            Account   = request.Account,
            Token    = request.Token,
            Error    = verifyResult,
        };
    }

    //[HttpPost( "refresh_token" )]
    //public async Task< VerifyTokenRes > RefreshToken( [FromBody] VerifyTokenReq request )
    //{
    //    var verifyResult = await _authService.VerifyToken( request.UserId, request.Token );

    //    _logger.LogInformation( $"Verifying token for user {request.UserId}" );

    //    return new()
    //    {
    //        UserId = request.UserId,
    //        Token  = request.Token,
    //    };
    //}
}
