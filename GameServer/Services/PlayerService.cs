using GameServer.Repository;
using GameServer.Services.Interface;
using HiveServer.Services;
using Shared;
using System.Net.Http;

namespace GameServer.Services;

public class PlayerService : IPlayerService
{
    private readonly ILogger< PlayerService > _logger;
    private readonly IMemoryDB _memoryDB;
    private readonly IGameDB _gameDB;

    public PlayerService( ILogger< PlayerService > logger, IGameDB gameDB, IMemoryDB memoryDB )
    {
        _gameDB = gameDB;
        _memoryDB = memoryDB;
        _logger = logger;
    }

    public async Task< ( ErrorCode, PlayerData ) > LoadPlayer( long userId )
    {
        var ( _, playerData ) = await _gameDB.LoadPlayer( userId );
        if ( playerData is null )
            return ( ErrorCode.GamePlayerIsNull, null );

        return ( ErrorCode.None, playerData );
    }

    public async Task< ErrorCode > CachePlayer( long userId, PlayerData userData )
    {
        return ErrorCode.None;
    }

    public async Task< ErrorCode > CreatePlayer( long userId, string nickName )
    {
        var errorCode = await _gameDB.CreatePlayer( userId, nickName );
        return errorCode;
    }
}
