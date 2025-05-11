using CloudStructures;
using CloudStructures.Structures;
using Microsoft.Extensions.Options;
using Shared;
using StackExchange.Redis;
using System;

namespace GameServer.Repository;

public class MemoryDB : IMemoryDB
{
    private readonly RedisConnection _redisConn;
    private readonly ILogger< MemoryDB > _logger;


    public MemoryDB( ILogger< MemoryDB > logger, IOptions< DBConfig > dbConfig )
    {
        _logger = logger;
        var config = new RedisConfig( "default", dbConfig.Value.Redis );
        _redisConn = new RedisConnection( config );
    }

    public class PlayerCacheModel
    {
        public long UserId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public async Task< ErrorCode > CachePlayer( long userId, string nickName, string token )
    {
        var stringSetCmd = new RedisString< PlayerCacheModel >( _redisConn, new( $"PlayerCache_{nickName}" ), TimeSpan.FromHours( 1 ) );
        var setResult = await stringSetCmd.SetAsync( new PlayerCacheModel()
        {
            UserId = userId,
            NickName = nickName,
            Token = token,
        } );

        if ( setResult )
        {
            _logger.LogInformation( $"Player {userId} cached successfully." );
        }
        else
        {
            _logger.LogError( $"Failed to cache player {userId}." );
            return ErrorCode.UnknownError;
        }

        return ErrorCode.None;
    }

    public async Task< ( ErrorCode, long ) > GetUserIdUsingNickName( string nickName )
    {
        var stringGetCmd = new RedisString< PlayerCacheModel >( _redisConn, new( $"PlayerCache_{nickName}" ), TimeSpan.FromHours( 1 ) );
        var getResult = await stringGetCmd.GetAsync();

        if ( !getResult.HasValue )
        {
            _logger.LogWarning( $"Player with nickname {nickName} not found in cache." );
            return ( ErrorCode.GameCachePlayerIsNull, 0 );
        }

        return ( ErrorCode.None, getResult.Value.UserId );
    }

    public void Dispose()
    {
    }

}
