namespace MatchServer.DTO;

public record CancelMatchReq
{
    public string Account { get; set; }
    public string Token { get; set; }
}
