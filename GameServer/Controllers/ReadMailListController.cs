using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReadMailListController : ControllerBase
{
    private readonly ILogger< ReadMailListController > _logger;
    private readonly IMailService _mailService;
    private readonly IAuthService _authService;

    public ReadMailListController( ILogger< ReadMailListController > logger, IMailService mailService, IAuthService authService )
    {
        _logger = logger;
        _authService = authService;
        _mailService = mailService;
    }

    [HttpPost]
    public async Task< ReadMailListRes > Post( [FromBody] ReadMailListReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !verifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"Read Mail failed: {verifyResult}" );
            return new(){ Error = verifyResult };
        }

        var ( readResult, mailList ) = await _mailService.ReadMailList( userId, request.LastReadMailId );
        return new()
        {
            Error = readResult,
            MailList = mailList
        };
    }
}
