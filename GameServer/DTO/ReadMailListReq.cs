namespace GameServer.DTO;

public record ReadMailListReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public int LastReadMailId { get; set; }
}
