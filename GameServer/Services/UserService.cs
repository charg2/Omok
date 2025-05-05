using GameServer.Repository;
using GameServer.Services.Interface;
using Shared;

namespace HiveServer.Services;

public class UserLoadData
{
    public long UserId{ get; set; } = 0;
    public int GamePlayCount { get; set; } = 0;
    public int GameWinCount { get; set; } = 0;
    public int GameLoseCount { get; set; } = 0;
    public int GameDrawCount { get; set; } = 0;
}


public class UserService : IUserService
{
    private readonly ILogger< UserService > _logger;
    private readonly IMemoryDB _memoryDB;
    private readonly IGameDB _gameDB;
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService( ILogger< UserService > logger, IGameDB gameDB, IMemoryDB memoryDB, IHttpClientFactory httpClientFactory )
    {
        _gameDB = gameDB;
        _memoryDB = memoryDB;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task< ( ErrorCode, UserLoadData ) > Login( string account, string token )
    {
        _logger.LogInformation( "Doing something..." );

        return ( ErrorCode.None, new UserLoadData() );
    }

    public void VerifyUserFromHiveServer( string account, string token )
    {
        /// Hive서버로 부터 토큰 유효성을 검증함
        using var client = _httpClientFactory.CreateClient();

        _logger.LogInformation( "Doing something..." );
    }

    public void LoadUserFromDB( long userId )
    {
        _logger.LogInformation( "Doing something..." );
    }
}
