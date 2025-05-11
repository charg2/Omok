
using GameServer.Model;
using GameServer.Services.Interface;
using Shared;

namespace GameServer.Repository;

public interface IGameDB : IDisposable
{
    Task< ErrorCode > CreatePlayer( long userId, string nickName );
    Task< ( ErrorCode, long userId ) > GetUserIdUsingNickName( string receiver );
    Task< ( ErrorCode, PlayerModel ) > LoadPlayer( long userId );

    Task< ErrorCode > CreateMail( SendMailParam param );
    Task< ( ErrorCode, List< MailModel > ) > ReadMailList( long userId, int lastReadMailId );

    Task< ErrorCode > AcceptFriend( long ownerId, long friendId );
    Task< ErrorCode > RemoveFriend( long ownerId, long friendId );
    Task< ( ErrorCode, List< FriendModel > ) > ReadFriendList( long userId );
    Task< ErrorCode > InviteFriend( long userId, long friendId );
}
