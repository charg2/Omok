
using GameServer.Model;
using GameServer.Services.Interface;
using Shared;

namespace GameServer.Repository;

public interface IGameDB : IDisposable
{
    public Task< ErrorCode > CreatePlayer( long userId, string nickName );
    public Task< ( ErrorCode, long playerId ) > GetUserIdUsingNickName( string receiver );
    public Task< ( ErrorCode, PlayerModel ) > LoadPlayer( long userId );

    public Task< ErrorCode > CreateMail( SendMailParam param );
    public Task< ( ErrorCode, List< MailModel > ) > ReadMailList( long userId, int lastReadMailId );

}
