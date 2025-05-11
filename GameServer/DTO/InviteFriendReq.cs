namespace GameServer.DTO;

public record InviteFriendReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string FriendName { get; set; } = string.Empty;
}
