using GameServer.Model;
using GameServer.Repository;
using GameServer.Services.Interface;
using FakeHiveServer.Services;
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

    public async Task< ( ErrorCode, PlayerModel ) > LoadPlayer( long userId )
    {
        var ( _, playerData ) = await _gameDB.LoadPlayer( userId );
        if ( playerData is null )
            return ( ErrorCode.GamePlayerIsNull, null );

        return ( ErrorCode.None, playerData );
    }

    public async Task< ErrorCode > CachePlayer( long userId, PlayerModel userData )
    {
        return ErrorCode.None;
    }

    public async Task< ErrorCode > CreatePlayer( long userId, string nickName )
    {
        var errorCode = await _gameDB.CreatePlayer( userId, nickName );
        return errorCode;
    }

    public async Task< ( ErrorCode, long playerId ) > GetUserIdUsingNickName( string receiverNickName )
    {
        var ( errorCode, userId ) = await _gameDB.GetUserIdUsingNickName( receiverNickName );
        if ( !errorCode.IsSuccess() )
        {
            _logger.LogWarning( $"ConvertPlayerId failed: {errorCode}" );
            return ( errorCode, 0 );
        }

        return ( ErrorCode.None, userId );
    }
}

