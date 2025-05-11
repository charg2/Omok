using Shared;

namespace MatchServer.DTO;

public record StartMatchRes
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
