using Shared;

namespace GameServer.DTO;

public record AcceptFriendRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
