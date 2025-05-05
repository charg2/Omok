using Shared;

namespace GameServer.Models.User;

public class CreatePlayerRes
{
    public long UserId { get; set; } = 0;
    public long PlayerId { get; set; } = 0;
    public string NickName { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
