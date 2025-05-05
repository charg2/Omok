using Shared;

namespace HiveServer.Models.User;

public class RegisterRes
{
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
