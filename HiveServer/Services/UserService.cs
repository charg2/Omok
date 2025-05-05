using HiveServer.Services.Interface;
using Shared;

namespace HiveServer.Services;


public class UserService : IUserService
{
    private readonly ILogger< UserService > _logger;
    private readonly IHiveDB _hiveDB;

    public UserService( IHiveDB hiveDB, ILogger<UserService> logger )
    {
        _hiveDB = hiveDB;
        _logger = logger;
    }

    public async Task< ( ErrorCode, string token ) > VaerifyUser( string account, string password )
    {
        try
        {
            //using var tx = _hiveDB.BeginTxAsync();

            var verifyResult = await _hiveDB.VerifyAccount( account, password );
            if ( verifyResult.HasError() )
            {
                //tx.Rollback();
                return ( verifyResult, string.Empty );
            }

            var token = TokenIssuer.Issue( account );

            var saveResult = await _hiveDB.SaveToken( account, token );
            if ( saveResult.HasError() )
            {
                //tx.Rollback();
                return ( saveResult, string.Empty );
            }

            return ( ErrorCode.None, token );
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error validate account for user {account}" );
            return ( ErrorCode.UnknownError, string.Empty );
        }
    }

    public async Task< ErrorCode > RegisterUser( string account, string password )
    {
        try
        {
            //using var tx = _hiveDB.BeginTxAsync();

            var createResult = await _hiveDB.CreateUser( account, password );
            if ( createResult.HasError() )
            {
                //tx.Rollback();
                _logger.LogWarning( $"Account creation failed for user {account}: {createResult}" );
                return createResult;
            }

            var createToken = await _hiveDB.CreateToken( account );
            if ( createToken.HasError() )
            {
                //tx.Rollback();
                _logger.LogWarning( $"Token creation failed for user {account}: {createToken}" );
                return createToken;
            }

            return createResult;
        }
        catch ( Exception ex )
        {
            _logger.LogError( ex, $"Error creating account for user {account}" );
            return ErrorCode.UnknownError;
        }
    }


}
