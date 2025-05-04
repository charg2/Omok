using HiveServer.Services.Interface;
using Shared;

namespace HiveServer.Services;


public class AccountService : IAccountService
{
    private readonly ILogger< AccountService > _logger;
    private readonly IHiveDB _hiveDB;

    public AccountService( IHiveDB hiveDB, ILogger<AccountService> logger )
    {
        _hiveDB = hiveDB;
        _logger = logger;
    }

    public async Task< ( ErrorCode, string token ) > VaerifyAccount( string userId, string password )
    {
        try
        {
            var verifyResult = await _hiveDB.VerifyAccount( userId, password );
            if ( verifyResult != ErrorCode.None )
                return (verifyResult, string.Empty);

            var token = TokenIssuer.Issue( userId );

            var saveResult = await _hiveDB.SaveToken( userId, token );
            if ( saveResult != ErrorCode.None )
                return ( saveResult, string.Empty );

            return ( ErrorCode.None, token );
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error validate account for user {userId}" );
            return ( ErrorCode.UnknownError, string.Empty );
        }
    }

    public async Task< ErrorCode > RegisterAccount( string userId, string password )
    {
        try
        {
            var createResult = await _hiveDB.CreateAccount( userId, password );
            if ( createResult != ErrorCode.None )
            {
                _logger.LogWarning( $"Account creation failed for user {userId}: {createResult}" );
                return createResult;
            }

            var createToken = await _hiveDB.CreateToken( userId );
            if ( createToken != ErrorCode.None )
            {
                _logger.LogWarning( $"Token creation failed for user {userId}: {createToken}" );
                return createToken;
            }

            return createResult;
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error creating account for user {userId}" );
            return ErrorCode.UnknownError;
        }
    }


}
