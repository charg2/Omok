namespace GameServer.Models.User;

public class CreatePlayerReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public long UserId { get; set; } = 0;
    public string NickName { get; set; } = string.Empty;
}
