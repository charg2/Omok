
using Shared;

namespace GameServer.Repository;


public record PlayerData
{
    public long UserId { get; init; }
    public string NickName { get; init; } = string.Empty;
    public string Avatar { get; init; } = string.Empty;
    public int WinCount { get; init; }
    public int LoseCount { get; init; }
    public int PlayCount { get; init; }
}


public interface IGameDB : IDisposable
{
    public Task< ErrorCode > CreatePlayer( long userId, string nickName );
    public Task< ( ErrorCode, PlayerData ) > LoadPlayer( long userId );

}
