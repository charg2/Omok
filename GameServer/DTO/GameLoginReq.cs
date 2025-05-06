namespace GameServer.DTO;

public record GameLoginReq
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
