using Shared;

namespace GameServer.DTO;

public record RemoveFriendRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;

}
