using FakeHiveServer.Services;
using Shared;

namespace GameServer.Services.Interface;

public interface IGamePlayService
{
    public Task< ErrorCode > DoSomething( long userId );
}

public interface IAttendanceService
{
    public Task< ErrorCode > DoSomething( long userId );
}
