namespace GameServer.DTO;

public record SendMailReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public long SenderId { get; set; } = 0;
}
