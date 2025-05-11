namespace GameServer.Model;


public enum EFriendStatus
{
    None,
    Invite,
    Inviting,
    Mutual,
}

public record FriendModel
{
    public long Owner_Id { get; set; } = 0;
    public long Friend_Id { get; set; } = 0;
    public EFriendStatus status { get; set; } = EFriendStatus.None;
    public DateTime accept_time { get; set; }
}
