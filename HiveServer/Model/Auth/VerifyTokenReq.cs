namespace HiveServer.Model.Auth;

public class VerifyTokenReq
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
