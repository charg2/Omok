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

        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory( _dbConn, _compiler );
    }

    public IDbTransaction BeginTxAsync()
    {
        return _dbConn.BeginTransaction();
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

            return ErrorCode.None;
        }
    }

    public async Task< ErrorCode > CreateToken( string account )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "login_token" )
                .InsertAsync( new
                {
                    account = account,
                    hive_token = "",
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

    public async Task< ErrorCode > VerifyToken( string account, string password )
    {
        var loginToken = await _queryFactory.Query( "login_token" )
                .Select( "*" )
                .Where( "account", account )
                .FirstOrDefaultAsync();
        if ( loginToken is null )
            return ErrorCode.HiveVerifyTokenNullToken;

        return ErrorCode.None;
    }

    public async Task< ErrorCode > SaveToken( string account, string token )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "login_token" )
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
