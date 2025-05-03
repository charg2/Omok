using Microsoft.Extensions.Options;
using System.Data;
using SqlKata.Execution;
using SqlKata.Compilers;
using MySqlConnector;
using Shared;

namespace HiveServer.Repository;

public class HiveDB : IHiveDB
{
    private readonly IOptions< DBConfig > _dbConfig;
    private IDbConnection? _dbConn;
    private readonly MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;

    public HiveDB( IOptions< DBConfig > dbConfig )
    {
        _dbConfig = dbConfig;

        Open();

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

    public async Task< ErrorCode > Login( string userId, string password )
    {
        try
        {
            var account = await _queryFactory.Query( "account" )
                .Select( "*" )
                .Where( "user_id", userId )
                .FirstOrDefaultAsync();
            if ( account is null )
                return ErrorCode.None;

            if ( account.Password != password )
                return ErrorCode.None;

            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            return ErrorCode.None;
        }
    }

    public async Task< ErrorCode > Register( string userId, string password )
    {
        try
        {
            var affectedRows = await _queryFactory.Query( "account" )
                                                  .InsertAsync( new { UserId = userId, Password = password } );


            return ErrorCode.None;
        }
        catch ( Exception ex )
        {
            // Log the exception (ex) if needed

            return ErrorCode.None;
        }
    }

    public async Task< ErrorCode > VerifyToken( string userId, string password )
    {
        throw new NotImplementedException();
    }
}
