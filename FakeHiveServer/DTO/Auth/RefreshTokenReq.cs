namespace FakeHiveServer.DTO.Auth;

public record RefreshTokenReq
{
    public string Account { get; set; } = string.Empty;
    public string OldToken { get; set; } = string.Empty;
}
