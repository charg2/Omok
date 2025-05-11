using GameServer.Model;
using Shared;

namespace GameServer.Services.Interface;

public interface IFriendService
{
    Task< ErrorCode > InviteFriend( long userId, long friendId );
    Task< ErrorCode > AcceptFriend( long userId, long friendId );

    Task< ErrorCode > RemoveFriend( long userId, long friendId );
    Task< ( ErrorCode, List< FriendModel > ) > ReadFriendList( long userId );

}
