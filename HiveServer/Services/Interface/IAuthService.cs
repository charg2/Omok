using Shared;

namespace HiveServer.Services.Interface;

public interface IAuthService
{
    public Task< ErrorCode > VerifyToken( string userId, string token );

}
