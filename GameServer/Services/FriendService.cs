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

    public Task< ErrorCode > ReadFriendList( long userId )
    {
        throw new NotImplementedException();
    }
}


