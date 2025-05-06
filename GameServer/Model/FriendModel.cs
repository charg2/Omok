namespace GameServer.Model;


public enum EFriendStatus
{
    None = 0,
    Request = 1,
    Accept = 2,
}

public record FriendModel
{
    public long Owner_Id { get; set; } = 0;
    public long Friend_Id { get; set; } = 0;
    public EFriendStatus Status { get; set; } = EFriendStatus.None;
}
