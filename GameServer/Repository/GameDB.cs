using GameServer.Model;
using GameServer.Services.Interface;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

namespace GameServer.Repository;


public class GameDB : IGameDB
{
    private readonly IOptions< DBConfig > _dbConfig;
    private IDbConnection? _dbConn;
    private readonly MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;
    private readonly ILogger< GameDB > _logger;

    public GameDB( ILogger< GameDB > logger, IOptions< DBConfig > dbConfig )
    {
        _logger   = logger;
        _dbConfig = dbConfig;

        Open();

        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory( _dbConn, _compiler );
    }

    private void Open()
    {
        _dbConn = new MySqlConnection( _dbConfig.Value.GameDB );

        _dbConn.Open();
    }

    private void Close()
    {
        _dbConn?.Close();
    }

    public void Dispose()
    {
        Close();
    }

    public async Task< ErrorCode > CreatePlayer( long userId, string nickName )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "player" )
                .InsertAsync( new
                {
                    user_id = userId,
                    nick_name = nickName,
                } );
            if ( affectedRows < 1 )
            {
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.GamePlayerAlreadyExists;
            }

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Query Error Occur! Reason: {ex.Message}\n {ex.StackTrace}" );
            return ErrorCode.UnknownError;
        }
    }

    public async Task< ( ErrorCode, PlayerModel ) > LoadPlayer( long userId )
    {
        try
        {
            var player = await _queryFactory.Query( "player" )
                .Select( "*" )
                .Where( "user_id", userId )
                .FirstOrDefaultAsync();
            if ( player is null )
                return ( ErrorCode.GamePlayerIsNull, null );

            return ( ErrorCode.None, new()
            {
                UserId     = player.user_id,
                NickName   = player.nick_name,
                CreateTime = player.create_time,
            } );
        }
        catch ( Exception ex )
        {
            return ( ErrorCode.UnknownError, null );
        }
    }

    public async Task< ( ErrorCode, long playerId ) > GetUserIdUsingNickName( string nickName )
    {
        try
        {
            var playerId = await _queryFactory.Query( "player" )
                .Select( "*" )
                .Where( "nick_name", nickName )
                .FirstOrDefaultAsync< long >();
            if ( playerId == 0 )
                return ( ErrorCode.GamePlayerIsNull, 0 );

            return ( ErrorCode.None, playerId );
        }
        catch ( Exception ex )
        {
            return ( ErrorCode.UnknownError, 0 );
        }
    }

    public async Task< ErrorCode > CreateMail( SendMailParam param )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "mail" )
                .InsertAsync( new
                {
                    owner_id   = param.ReceiverId,
                    sender_id  = param.SenderId,
                    title      = param.Title,
                    content    = param.Content,
                } );
            if ( affectedRows < 1 )
            {
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.GamePlayerAlreadyExists;
            }

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Query Error Occur! Reason: {ex.Message}\n {ex.StackTrace}" );
            return ErrorCode.UnknownError;
        }
    }


    public async Task< ( ErrorCode, List< MailModel > ) > ReadMailList( long userId, int lastReadMailId )
    {
        try
        {
            var mailList = await _queryFactory.Query( "mail" )
                .Select( "*" )
                .Where( "owner_id", userId )
                .Where( "id", ">", lastReadMailId )
                .GetAsync< MailModel >();

            if ( mailList is null )
                return ( ErrorCode.GamePlayerIsNull, null );

            return ( ErrorCode.None, mailList.ToList() );
        }
        catch ( Exception ex )
        {
            return ( ErrorCode.UnknownError, null );
        }
    }


}
