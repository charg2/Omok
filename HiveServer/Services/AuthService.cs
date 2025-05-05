using HiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace HiveServer.Services;


public class AuthService : IAuthService
{
    private readonly ILogger< AuthService > _logger;
    private readonly IUserDB _userDB;

    public AuthService( IUserDB hiveDB, ILogger< AuthService > logger )
    {
        _userDB = hiveDB;
        _logger = logger;
    }

    public async Task< ( ErrorCode, long ) > VerifyToken( string account, string token )
    {
        try
        {
            var ( result, userId ) = await _userDB.VerifyToken( account, token );
            if ( result != ErrorCode.None )
            {
                _logger.LogWarning( $"Token verification failed for user {account}: {result}" );
                return ( result, 0 );
            }

            return ( result, userId );
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error creating account for user {account}" );
            return ( ErrorCode.HiveVerifyTokenExecptionOccur, 0 );
        }
    }
}
