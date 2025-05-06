namespace GameServer.Model;

public record PlayerModel
{
    public long UserId { get; set; } = 0;
    public string NickName { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
    public DateTime LastLoginTime { get; set; }
}
