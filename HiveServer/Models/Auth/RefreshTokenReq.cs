namespace HiveServer.Models.Auth;

public class RefreshTokenReq
{
    public string Account { get; set; } = string.Empty;
    public string OldToken { get; set; } = string.Empty;
}
