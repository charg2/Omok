using Shared;

namespace GameServer.Services.Interface;

public interface IAuthService
{
    Task< ( ErrorCode, long ) > VerifyToken( string account, string token );

    Task< ( ErrorCode, long userId ) > VerifyTokenAndGetUserId( string account, string token );

}
