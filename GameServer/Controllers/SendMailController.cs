using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GameServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SendMailController : ControllerBase
{
    private readonly ILogger< SendMailController > _logger;
    private readonly IMailService _mailService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public SendMailController( ILogger< SendMailController > logger, IMailService mailService, IPlayerService playerService, IAuthService authService )
    {
        _logger = logger;
        _playerService = playerService;
        _mailService = mailService;
        _authService = authService;
    }

    [HttpPost]
    public async Task< SendMailRes > Post( [FromBody] SendMailReq request )
    {
        var ( verifyResult, userId ) = await _authService.VerifyToken( request.Account, request.Token );
        if ( !verifyResult.IsSuccess() )
        {
            _logger.LogWarning( $"SendMail failed: {verifyResult}" );
            return new(){ ErrorCode = verifyResult };
        }

        var ( getResult, receiverId ) = await _playerService.GetUserIdUsingNickName( request.Receiver );
        if ( !getResult.IsSuccess() )
        {
            _logger.LogWarning( $"SendMail failed: {getResult}" );
            return new(){ ErrorCode = getResult };
        }

        var sendMailError = await _mailService.SendMail(
            new SendMailParam()
            {
                SenderId   = userId,
                Content    = request.Content,
                Title      = request.Title,
                ReceiverId = receiverId,
            } );

        return new(){ ErrorCode = sendMailError };
    }
}
