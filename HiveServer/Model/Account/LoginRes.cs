using Shared;

namespace HiveServer.Model.Account;

public class LoginRes
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
