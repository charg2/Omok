
using Shared;

namespace HiveServer.Services.Interface;

public interface IAccountService
{
    public Task< ErrorCode > Login( string userId, string password );
    public Task< ErrorCode > Register( string userId, string password );
}