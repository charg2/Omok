using Microsoft.Extensions.Options;
using System.Data;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using MySqlConnector;
using Shared;

namespace FakeHiveServer.Repository;

public class UserDB : IUserDB
{
    private readonly IOptions< DBConfig > _dbConfig;
    private IDbConnection? _dbConn;
    private MySqlTransaction? _dbTx;
    private readonly MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;
    private readonly ILogger< UserDB > _logger;

    public UserDB( ILogger< UserDB > logger, IOptions< DBConfig > dbConfig )
    {
        _logger   = logger;
        _dbConfig = dbConfig;

        Open();

        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory( _dbConn, _compiler );
    }

    public IDbTransaction BeginTxAsync()
    {
        return _dbConn.BeginTransaction();
    }

    private void Open()
    {
        _dbConn = new MySqlConnection( _dbConfig.Value.UserDB );

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

    public async Task< ErrorCode > VerifyAccount( string account, string password )
    {
        try
        {
            var user = await _queryFactory.Query( "user" )
                .Select( "*" )
                .Where( "account", account )
                .FirstOrDefaultAsync();
            if ( user is null )
                return ErrorCode.HiveLoginNullUserId;

            if ( user.password != password )
                return ErrorCode.HiveLoginInvalidPassword;

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Error Occur! {ex.Message}" );
            return ErrorCode.None;
        }
    }

    public async Task< ErrorCode > CreateUser( string account, string password )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "user" )
                .InsertAsync( new
                {
                    account  = account,
                    password = password,
                    create_time = DateTime.Now,
                } );
            if ( affectedRows < 1 )
                return ErrorCode.HiveLoginCreateAccountFail;

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Error Occur! {ex.Message}" );

            return ErrorCode.UnknownError;
        }
    }

    public async Task< ErrorCode > CreateToken( string account )
    {
        try
        {
            long userId = await _queryFactory.Query( "user" )
                .Select( "id" )
                .Where( "account", account )
                .FirstOrDefaultAsync< long >();
            if (  userId == 0 )
            {
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.HiveLoginCreateTokenFail;
            }

            var affectedRows = await _queryFactory.Query( "auth_token" )
                .InsertAsync( new
                {
                    account = account,
                    hive_token = "",
                    user_id = userId,
                    create_time = DateTime.Now,
                    expire_time = DateTime.Now,
                } );
            if ( affectedRows < 1 )
            {
                _logger.LogError( $"DB Error Occur!" );
                return ErrorCode.HiveLoginCreateTokenFail;
            }

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Error Occur! {ex.Message}" );

            return ErrorCode.UnknownError;
        }
    }

    public async Task< ( ErrorCode, long userId ) > VerifyToken( string account, string token )
    {
        var authToken = await _queryFactory.Query( "auth_token" )
                .Select( "*" )
                .Where( "account", account )
                .FirstOrDefaultAsync();
        if ( authToken is null )
            return ( ErrorCode.HiveVerifyTokenNullToken, 0 );

        if ( authToken.hive_token != token )
            return ( ErrorCode.HiveVerifyTokenInvalid, 0 );

        return ( ErrorCode.None, authToken.user_id );
    }

    public async Task< ErrorCode > SaveToken( string account, string token )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "auth_token" )
                .UpdateAsync( new{ account = account, hive_token = token } );
            if ( affectedRows < 1 )
                return ErrorCode.HiveLoginSaveTokenFail;

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            _logger.LogError( $"DB Error Occur!" );
            return ErrorCode.HiveLoginSaveTokenFail;
        }
    }


}
