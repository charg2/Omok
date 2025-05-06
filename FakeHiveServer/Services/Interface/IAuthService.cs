using Shared;

namespace FakeHiveServer.Services.Interface;

public interface IAuthService
{
    public Task< ( ErrorCode, long userId ) > VerifyToken( string account, string token );

}
