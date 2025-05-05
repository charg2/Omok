using HiveServer.Services;
using Shared;

namespace GameServer.Services.Interface;

public interface IUserService
{
    public Task< ( ErrorCode, UserLoadData ) > Login( string account, string token );

}
