namespace MatchServer.DTO;

public record StartMatchReq
{
    public string Account { get; set; }
    public string Token { get; set; }
}
