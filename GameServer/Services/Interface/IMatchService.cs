using Shared;

namespace GameServer.Services.Interface;

public interface IMatchService
{
    Task< ErrorCode > StartMatch( string account, string token );
    Task< ErrorCode > CancelMatch( string account, string token );
}
