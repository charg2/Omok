using Shared;

namespace HiveServer.Model.Auth;

public class VerifyTokenRes
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
