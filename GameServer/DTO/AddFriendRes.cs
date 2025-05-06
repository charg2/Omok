using Shared;

namespace GameServer.DTO;

public record AddFriendRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;

}
