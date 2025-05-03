using HiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HiveServer.Services;


public class AuthService : IAuthService
{
    private readonly ILogger< AuthService > _logger;
    private readonly IHiveDB _hiveDB;

    public AuthService( IHiveDB hiveDB, ILogger< AuthService > logger )
    {
        _hiveDB = hiveDB;
        _logger = logger;
    }

    public async Task< ErrorCode > VerifyToken( string userId, string password, string token )
    {
        try
        {
            _hiveDB.VerifyToken( userId, password );
            return ErrorCode.UnknownError;
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, "Error creating account for user {UserId}", userId );
            return ErrorCode.UnknownError;
        }
    }

}
