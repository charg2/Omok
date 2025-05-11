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

    public async Task< ErrorCode > CreatePlayer( long userId, string nickName )
    {
        var errorCode = await _gameDB.CreatePlayer( userId, nickName );
        return errorCode;
    }

    public async Task< ( ErrorCode, long userId ) > GetUserIdUsingNickName( string receiverNickName )
    {
        var ( getCacheResult, cacheUserId ) = await _memoryDB.GetUserIdUsingNickName( receiverNickName );
        if ( cacheUserId != 0 )
            return ( ErrorCode.None, cacheUserId );

        var ( getDBError, dbUserId ) = await _gameDB.GetUserIdUsingNickName( receiverNickName );
        if ( !getDBError.IsSuccess() )
        {
            _logger.LogWarning( $"Get UserId Failed: {getDBError}" );
            return ( getDBError, 0 );
        }

        return ( ErrorCode.None, dbUserId );
    }

    public async Task< ErrorCode > CachePlayer( long userId, string nickName, string token )
    {
        var errorCode = await _memoryDB.CachePlayer( userId, nickName, token );
        return errorCode;
    }
}

