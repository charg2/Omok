using GameServer.Model;
using Shared;

namespace GameServer.DTO;

public record ReadMailListRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;
    public List< MailModel > MailList { get; set; }
}
