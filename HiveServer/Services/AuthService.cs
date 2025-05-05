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

    public async Task< ErrorCode > VerifyToken( string account, string token )
    {
        try
        {
            var result = await _hiveDB.VerifyToken( account, token );
            if ( result != ErrorCode.None )
            {
                _logger.LogWarning( $"Token verification failed for user {account}: {result}" );
                return result;
            }

            return result;
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error creating account for user {account}" );
            return ErrorCode.HiveVerifyTokenExecptionOccur;
        }
    }
}
