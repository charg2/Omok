using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using Microsoft.Extensions.Options;
using Shared;

namespace FakeHiveServer.Services;


public class AuthService : IAuthService
{
    private readonly ILogger< AuthService > _logger;
    private readonly IMemoryDB _memoryDB;
    private readonly IGameDB _gameDB;
    private readonly HttpClient _httpClient;
    private readonly IOptions< ServiceConfig > _serviceConfig;

    public AuthService( ILogger< AuthService > logger, IOptions< ServiceConfig > serviceConfig, IGameDB gameDB, IMemoryDB memoryDB, HttpClient httpClient )
    {
        _logger = logger;
        _gameDB = gameDB;
        _memoryDB = memoryDB;
        _serviceConfig = serviceConfig;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri( _serviceConfig.Value.FakeHiveServer );
    }

    public async Task< ( ErrorCode, long ) > VerifyToken( string account, string token )
    {
        var response = await _httpClient.PostAsJsonAsync( "/api/VerifyToken", new VerifyTokenReq()
        {
            Account = account,
            Token = token
        } );

        if ( !response.IsSuccessStatusCode )
        {
            _logger.LogWarning( $"Token verification failed for user {account}: {response.StatusCode}" );
            return new();
        }

        var teoknVerifyRes = await response.Content.ReadFromJsonAsync< VerifyTokenRes >();
        if ( teoknVerifyRes is null )
        {
            _logger.LogWarning( $"Token verification failed for user {account}" );
            return new();
        }

        return ( ErrorCode.None, teoknVerifyRes.UserId );
    }

    public async Task< ( ErrorCode, long userId ) > VerifyTokenAndGetUserId( string account, string token )
    {
        throw new NotImplementedException();
    }
}
