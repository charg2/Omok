using Shared;

namespace GameServer.DTO;

public record VerifyTokenRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public long UserId { get; set; } = 0;
}
