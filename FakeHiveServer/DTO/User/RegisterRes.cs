using Shared;

namespace FakeHiveServer.DTO.User;

public class RegisterRes
{
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
}
