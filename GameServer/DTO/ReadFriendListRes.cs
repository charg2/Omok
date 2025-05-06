using GameServer.Model;
using Shared;

namespace GameServer.DTO;

public record ReadFriendListRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;
    public List< FriendModel > FriendList { get; internal set; }
}
