using GameServer.Model;
using GameServer.Services.Interface;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Pipelines.Sockets.Unofficial.Buffers;
using Shared;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

namespace GameServer.Repository;


public partial class GameDB : IGameDB
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
            var pc = await _queryFactory.Query( "player" )
                .Select( "*" )
                .Where( "user_id", userId )
                .FirstOrDefaultAsync();
            if ( pc is null )
                return ( ErrorCode.GamePlayerIsNull, null );

            return ( ErrorCode.None, new()
            {
                UserId     = pc.user_id,
                NickName   = pc.nick_name,
                CreateTime = pc.create_time,
            } );
        }
        catch ( Exception ex )
        {
            return ( ErrorCode.UnknownError, null );
        }
    }

    public async Task< ( ErrorCode, long userId ) > GetUserIdUsingNickName( string nickName )
    {
        try
        {
            var userId = await _queryFactory.Query( "player" )
                .Select( "user_id" )
                .Where( "nick_name", nickName )
                .FirstOrDefaultAsync< long >();
            if ( userId is 0 )
                return ( ErrorCode.GamePlayerIsNull, 0 );

            return ( ErrorCode.None, userId );
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

public partial class GameDB : IGameDB
{
    public async Task< ErrorCode > RemoveFriend( long ownerId, long friendId )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "friend" )
                .Where( "owner_id", ownerId )
                .Where( "friend_id", friendId )
                .DeleteAsync();
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

    public async Task< ( ErrorCode, List< FriendModel > ) > ReadFriendList( long ownerId )
    {
        try
        {
            var friendList = await _queryFactory.Query( "friend" )
                .Select( "*" )
                .Where( "owner_id", ownerId )
                .GetAsync< FriendModel >();
            if ( friendList is null )
                return ( ErrorCode.GamePlayerIsNull, null );

            return ( ErrorCode.None, friendList.ToList() );
        }
        catch ( Exception ex )
        {
            return ( ErrorCode.UnknownError, null );
        }
    }

    public async Task< ErrorCode > InviteFriend( long userId, long friendId )
    {
        try
        {
            using var tx = _dbConn!.BeginTransaction();

            var affectedRows1 = await _queryFactory.Query( "friend" )
                .InsertAsync( new
                {
                    owner_id  = userId,
                    friend_id = friendId,
                    status = EFriendStatus.Invite,
                },
                tx );
            if ( affectedRows1 < 1 )
            {
                tx.Rollback();
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.GamePlayerAlreadyExists;
            }

            var affectedRows2 = await _queryFactory.Query( "friend" )
                .InsertAsync( new
                {
                    owner_id  = friendId,
                    friend_id = userId,
                    status = EFriendStatus.Inviting,
                },
                tx );
            if ( affectedRows2 < 1 )
            {
                tx.Rollback();
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.GamePlayerAlreadyExists;
            }

            tx.Commit();
            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Query Error Occur! Reason: {ex.Message}\n {ex.StackTrace}" );
            return ErrorCode.UnknownError;
        }
    }

    public async Task< ErrorCode > AcceptFriend( long ownerId, long friendId )
    {
        try
        {
            using var tx = _dbConn!.BeginTransaction();

            var affectedRows1 = await _queryFactory.Query( "friend" )
                .Where( "owner_id", ownerId )
                .Where( "friend_id", friendId )
                .Where( "status", EFriendStatus.Inviting )
                .UpdateAsync( new
                {
                    status = EFriendStatus.Mutual,
                    accept_time = DateTime.Now
                } );
            if ( affectedRows1 < 1 )
            {
                tx.Rollback();
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.GamePlayerAlreadyExists;
            }

            var affectedRows2 = await _queryFactory.Query( "friend" )
                .Where( "owner_id", friendId )
                .Where( "friend_id", ownerId )
                .Where( "status", EFriendStatus.Invite )
                .UpdateAsync( new
                {
                    status = EFriendStatus.Mutual,
                    accept_time = DateTime.Now
                } );
            if ( affectedRows2 < 1 )
            {
                tx.Rollback();
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.GamePlayerAlreadyExists;
            }

            tx.Commit();
            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Query Error Occur! Reason: {ex.Message}\n {ex.StackTrace}" );
            return ErrorCode.UnknownError;
        }
    }
}
