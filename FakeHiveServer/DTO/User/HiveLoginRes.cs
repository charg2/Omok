using Shared;

namespace FakeHiveServer.DTO.User;

public class HiveLoginRes
{
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
