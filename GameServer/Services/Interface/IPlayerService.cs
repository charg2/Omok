using GameServer.Model;
using Shared;

namespace GameServer.Services.Interface;

public interface IPlayerService
{
    public Task< ErrorCode > CreatePlayer( long userId, string nickName );
    public Task< ( ErrorCode, PlayerModel ) > LoadPlayer( long userId );
    public Task< ( ErrorCode, long playerId ) >  GetUserIdUsingNickName( string receiver );
}
