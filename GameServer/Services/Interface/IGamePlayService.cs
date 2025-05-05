using HiveServer.Services;
using Shared;

namespace GameServer.Services.Interface;

public interface IGamePlayService
{
    public Task< ErrorCode > DoSomething( long userId );
}

public interface IMatchingService
{
    public Task< ErrorCode > DoSomething( long userId );
}

public interface IAttendanceService
{
    public Task< ErrorCode > DoSomething( long userId );
}

public interface IFriendService
{
    public Task< ErrorCode > DoSomething( long userId );
}

public interface IChatService
{
    public Task< ErrorCode > DoSomething( long userId );
}

public interface IMailService
{
    public Task< ErrorCode > DoSomething( long userId );
}
