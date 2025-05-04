using Microsoft.Extensions.Options;
using System.Data;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using MySqlConnector;
using Shared;

namespace HiveServer.Repository;

public class HiveDB : IHiveDB
{
    private readonly IOptions< DBConfig > _dbConfig;
    private IDbConnection? _dbConn;
    private MySqlTransaction? _dbTx;
    private readonly MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;
    private readonly ILogger< HiveDB > _logger;

    public HiveDB( ILogger< HiveDB > logger, IOptions< DBConfig > dbConfig )
    {
        _logger   = logger;
        _dbConfig = dbConfig;

        Open();

        //_dbConn.BeginTransaction();
        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory( _dbConn, _compiler );
    }

    private void Open()
    {
        _dbConn = new MySqlConnection( _dbConfig.Value.HiveDB );

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

    public async Task< ErrorCode > VerifyAccount( string userId, string password )
    {
        try
        {
            var account = await _queryFactory.Query( "account" )
                .Select( "*" )
                .Where( "user_id", userId )
                .FirstOrDefaultAsync();
            if ( account is null )
                return ErrorCode.HiveLoginNullUserId;

            if ( account.Password != password )
                return ErrorCode.HiveLoginInvalidPassword;

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            return ErrorCode.None;
        }
    }

    public async Task< ErrorCode > CreateAccount( string userId, string password )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "account" )
                .InsertAsync( new
                {
                    user_id  = userId,
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

            return ErrorCode.None;
        }
    }

    public async Task< ErrorCode > CreateToken( string userId )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "login_token" )
                .InsertAsync( new
                {
                    account_user_id = userId,
                    hive_token = "",
                    create_time = DateTime.Now,
                    expires_time = DateTime.Now,
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

    public async Task< ErrorCode > VerifyToken( string userId, string password )
    {
        var loginToken = await _queryFactory.Query( "login_token" )
                .Select( "*" )
                .Where( "user_id", userId )
                .FirstOrDefaultAsync();
        if ( loginToken is null )
            return ErrorCode.HiveVerifyTokenNullToken;

        return ErrorCode.None;
    }

    public async Task< ErrorCode > SaveToken( string userId, string token )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "login_token" )
                .UpdateAsync( new{ UserId = userId, Token = token } );
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
