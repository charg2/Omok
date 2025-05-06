namespace GameServer.Model;

public record MailModel
{
    public long Owner_Id { get; set; } = 0;

    public long Id { get; set; } = 0;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime Create_Time { get; set; }
}
