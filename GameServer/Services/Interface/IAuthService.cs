using Shared;

namespace GameServer.Services.Interface;

public interface IAuthService
{
    public Task< ( ErrorCode, long ) > VerifyToken( string account, string token );

    public Task< ( ErrorCode, long userId ) > VerifyTokenAndGetUserId( string account, string token );

}
