using Shared;

namespace HiveServer.Models.Auth;

public class RefreshTokenRes
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
