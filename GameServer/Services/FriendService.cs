using GameServer.Model;
using GameServer.Repository;
using GameServer.Services.Interface;
using Shared;

namespace GameServer.Services;

public class FriendService : IFriendService
{
    private readonly ILogger< FriendService > _logger;
    private readonly IGameDB _gameDB;

    public FriendService( ILogger< FriendService > logger, IGameDB gameDB )
    {
        _logger = logger;
        _gameDB = gameDB;
    }

    public async Task< ErrorCode > InviteFriend( long userId, long friendId )
    {
        return await _gameDB.InviteFriend( userId, friendId );
    }

    public async Task< ErrorCode > AcceptFriend( long userId, long friendId )
    {
        return await _gameDB.AcceptFriend( userId, friendId );
    }

    public async Task< ErrorCode > RemoveFriend( long userId, long friendId )
    {
        return await _gameDB.RemoveFriend( userId, friendId );
    }

    public async Task< ( ErrorCode, List< FriendModel > ) > ReadFriendList( long userId )
    {
        var ( error, friendList ) = await _gameDB.ReadFriendList( userId );
        if ( !error.IsSuccess() )
        {
            _logger.LogWarning( $"ReadFriendList failed: {error}" );
            return ( error, null );
        }

        return ( error, friendList );
    }

}


