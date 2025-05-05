using GameServer.Repository;
using HiveServer.Services;
using Shared;

namespace GameServer.Services.Interface;

public interface IPlayerService
{
    public Task< ErrorCode > CreatePlayer( long userId, string nickName );
    public Task< ( ErrorCode, PlayerData ) > LoadPlayer( long userId );

}
