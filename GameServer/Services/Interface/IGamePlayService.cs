using FakeHiveServer.Services;
using Shared;

namespace GameServer.Services.Interface;

public interface IGamePlayService
{
    Task< ErrorCode > DoSomething( long userId );
}

public interface IAttendanceService
{
    Task< ErrorCode > DoSomething( long userId );
}
