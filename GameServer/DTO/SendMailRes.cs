using Shared;

namespace GameServer.DTO;

public record SendMailRes
{
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;

    /// 남은 재화 정보
}
