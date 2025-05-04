using Microsoft.Extensions.Options;
using System.Data;
using SqlKata.Execution;
using SqlKata.Compilers;
using MySqlConnector;

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

    public async Task< bool > CreateAccount( string userId, string password )
    {
        int affectedRows = 0;

        try
        {
            affectedRows = await _queryFactory.Query( "account" )
                .InsertAsync( new{ UserId = userId, Password = password } );
        }
        catch ( Exception ex )
        {
            // Log the exception (ex) if needed
            return false;
        }

        return affectedRows > 0;
    }
}
