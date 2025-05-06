namespace GameServer.DTO;

public record RemoveFriendReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string FriendName { get; set; } = string.Empty;
}
