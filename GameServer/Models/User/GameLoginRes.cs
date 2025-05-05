using GameServer.Repository;
using HiveServer.Services;
using Shared;

namespace GameServer.Models.User;

public class GameLoginRes
{
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ErrorCode Error { get; set; } = ErrorCode.None;
    public PlayerData PlayerData { get; set; }
}
