public class ServiceConfig
{
    public string FakeHiveServer { get; set; } = string.Empty;
    public string GameServer { get; set; } = string.Empty;
    public string MatchServer { get; set; } = string.Empty;

    public string VerifyTokenUrl { get; set; } = "/api/VerifyToken";
    public string StartMatchUrl { get; set; } = "/api/StartMatch";
    public string CancelMatchUrl { get; set; } = "/api/CancelMatch";
}

