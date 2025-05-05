using Shared;

namespace GameServer.Models.User;

public class VerifyTokenRes
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public long UserId { get; set; } = 0;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
