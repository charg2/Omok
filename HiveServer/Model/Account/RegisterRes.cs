using Shared;

namespace HiveServer.Model.Account;

public class RegisterRes
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
