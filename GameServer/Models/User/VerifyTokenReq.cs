namespace GameServer.Models.User;

public class VerifyTokenReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
