using Shared;

namespace GameServer.DTO;

public record InviteFriendRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
