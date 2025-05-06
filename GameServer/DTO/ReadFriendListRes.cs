using Shared;

namespace GameServer.DTO;

public record ReadFriendListRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;

}
