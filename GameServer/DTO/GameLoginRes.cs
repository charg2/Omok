using GameServer.Model;
using GameServer.Repository;
using Shared;

namespace GameServer.DTO;

public record GameLoginRes
{
    public ErrorCode Error { get; set; } = ErrorCode.None;
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public PlayerModel Player { get; set; }
}
