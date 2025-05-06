using GameServer.Model;
using Shared;

namespace GameServer.Services.Interface;

public interface IFriendService
{
    public Task< ErrorCode > AddFriend( long userId, long friendId );
    public Task< ErrorCode > RemoveFriend( long userId, long friendId );
    public Task< ( ErrorCode, List< FriendModel > ) > ReadFriendList( long userId );

}
