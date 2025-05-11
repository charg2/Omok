using GameServer.Repository;
using GameServer.Services.Interface;
using Microsoft.Extensions.Options;
using Shared;

namespace GameServer.Services;

public class ChatService : IChatService
{
    private readonly ILogger< ChatService > _logger;
    private readonly IMemoryDB _memoryDB;
    private readonly IGameDB _gameDB;
    private readonly IOptions< ServiceConfig > _serviceConfig;

    public ChatService( ILogger< ChatService > logger, IOptions< ServiceConfig > serviceConfig, IGameDB gameDB )
    {
        _logger = logger;
        _gameDB = gameDB;
        _serviceConfig = serviceConfig;
    }

    public async Task< ErrorCode > DoSomething( long userId )
    {
        throw new NotImplementedException();
    }
}
