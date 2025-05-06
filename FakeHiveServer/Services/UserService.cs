using FakeHiveServer.Services.Interface;
using Shared;

namespace FakeHiveServer.Services;


public class UserService : IUserService
{
    private readonly ILogger< UserService > _logger;
    private readonly IUserDB _userDB;

    public UserService( IUserDB hiveDB, ILogger<UserService> logger )
    {
        _userDB = hiveDB;
        _logger = logger;
    }

    public async Task< ( ErrorCode, string token ) > VaerifyUser( string account, string password )
    {
        try
        {
            //using var tx = _userDB.BeginTxAsync();

            var verifyResult = await _userDB.VerifyAccount( account, password );
            if ( !verifyResult.IsSuccess() )
            {
                //tx.Rollback();
                return ( verifyResult, string.Empty );
            }

            var token = TokenIssuer.Issue( account );

            var saveResult = await _userDB.SaveToken( account, token );
            if ( !saveResult.IsSuccess() )
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
            var createResult = await _userDB.CreateUser( account, password );
            if ( !createResult.IsSuccess() )
            {
                //tx.Rollback();
                _logger.LogWarning( $"Account creation failed for user {account}: {createResult}" );
                return createResult;
            }

            var createToken = await _userDB.CreateToken( account );
            if ( !createToken.IsSuccess() )
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
