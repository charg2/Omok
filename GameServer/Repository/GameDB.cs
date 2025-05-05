using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Security.Principal;

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


    public async Task< ( ErrorCode, PlayerData ) > LoadPlayer( long userId )
    {
        try
        {
            var loginToken = await _queryFactory.Query( "player" )
                .Select( "*" )
                .Where( "user_id", userId )
                .FirstOrDefaultAsync();
            if ( loginToken is null )
                return ( ErrorCode.GamePlayerIsNull, null );

            return ( ErrorCode.None, loginToken );
        }
        catch ( Exception ex )
        {
            return ( ErrorCode.UnknownError, null );
        }
    }
}
