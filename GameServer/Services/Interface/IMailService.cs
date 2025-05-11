using GameServer.Model;
using Shared;

namespace GameServer.Services.Interface;

public record class SendMailParam
{
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public long ReceiverId { get; init; } = 0;
    public long SenderId { get; internal set; } = 0;
}

public interface IMailService
{
    Task< ErrorCode > SendMail( SendMailParam param );

    Task< ( ErrorCode, List< MailModel > ) > ReadMailList( long lastReadMailId, int lastReadMailId1 );
}
