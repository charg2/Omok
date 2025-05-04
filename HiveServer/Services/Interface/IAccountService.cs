
using Shared;

namespace HiveServer.Services.Interface;

public interface IAccountService
{
    public Task< ( ErrorCode, string token ) > VaerifyAccount( string userId, string password );
    public Task< ErrorCode > RegisterAccount( string userId, string password );
}
