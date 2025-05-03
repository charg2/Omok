using HiveServer.Services.Interface;
using Shared;

namespace HiveServer.Services;


public class AccountService : IAccountService
{
    private readonly ILogger< AccountService > _logger;
    private readonly IHiveDB _hiveDB;

    public AccountService( IHiveDB hiveDB, ILogger< AccountService > logger )
    {
        _hiveDB = hiveDB;
        _logger = logger;
    }

    public async Task< ErrorCode > Login( string userId, string password )
    {
        try
        {
            _hiveDB.Login( userId, password );
            return ErrorCode.UnknownError;
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error creating account for user {userId}" );
            return ErrorCode.UnknownError;
        }
    }

    public async Task< ErrorCode > Register( string userId, string password )
    {
        try
        {
            _hiveDB.Register( userId, password );
            return ErrorCode.UnknownError;
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error creating account for user {userId}" );
            return ErrorCode.UnknownError;
        }
    }
}
