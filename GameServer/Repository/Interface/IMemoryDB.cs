using Shared;

namespace GameServer.Repository;

public interface IMemoryDB : IDisposable
{
    Task< ErrorCode > CachePlayer( long userId, string nickName, string token );
    Task< ( ErrorCode, long ) > GetUserIdUsingNickName( string nickName );
}
