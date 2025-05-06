namespace GameServer.DTO;

public record ReadFriendListReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
