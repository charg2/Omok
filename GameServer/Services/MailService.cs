using GameServer.Model;
using GameServer.Repository;
using GameServer.Services.Interface;
using Shared;

namespace GameServer.Services;


public class MailService : IMailService
{
    private readonly ILogger< MailService > _logger;
    private readonly IGameDB _gameDB;

    public MailService( ILogger< MailService > logger, IGameDB gameDB )
    {
        _logger = logger;
        _gameDB = gameDB;
    }

    public async Task< ErrorCode > SendMail( SendMailParam param )
    {
        var errorCode = await _gameDB.CreateMail( param );
        if ( !errorCode.IsSuccess() )
        {
            _logger.LogWarning( $"SendMail failed: {errorCode}" );
            return errorCode;
        }

        return ErrorCode.None;
    }

    public async Task< ( ErrorCode, List< MailModel > ) > ReadMailList( long userId, int lastReadMailId )
    {
        var ( error, mailList ) = await _gameDB.ReadMailList( userId, lastReadMailId );
        if ( !error.IsSuccess() )
        {
            _logger.LogWarning( $"ReadMailList failed: {error}" );
            return ( error, null );
        }

        return ( error, mailList );
    }
}
