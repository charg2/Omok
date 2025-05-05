namespace HiveServer.Models.User;

public class HiveLoginReq
{
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
