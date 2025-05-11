using GameServer.Model;
using Shared;

namespace GameServer.Services.Interface;

public interface IPlayerService
{
    Task< ErrorCode > CreatePlayer( long userId, string nickName );
    Task< ( ErrorCode, PlayerModel ) > LoadPlayer( long userId );
    Task< ( ErrorCode, long userId ) > GetUserIdUsingNickName( string receiver );
    Task< ErrorCode > CachePlayer( long userId, string nickName, string token );
}
