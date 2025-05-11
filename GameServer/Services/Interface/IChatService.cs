using Shared;

namespace GameServer.Services.Interface;

public interface IChatService
{
    public Task< ErrorCode > DoSomething( long userId );
}
