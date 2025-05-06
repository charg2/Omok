namespace GameServer.Model;

public record ItemModel
{
    public long OwnerId { get; set; } = 0;
    public DateTime CreateTime { get; set; }
}
