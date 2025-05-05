
using Shared;

namespace HiveServer.Services.Interface;

public interface IUserService
{
    public Task< ( ErrorCode, string token ) > VaerifyUser( string userId, string password );
    public Task< ErrorCode > RegisterUser( string userId, string password );
}
