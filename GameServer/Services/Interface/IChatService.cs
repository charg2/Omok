using Shared;

namespace GameServer.Services.Interface;

public interface IChatService
{
    Task< ErrorCode > DoSomething( long userId );
}
