using Shared;

namespace GameServer.DTO;

public record CancelMatchRes
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
