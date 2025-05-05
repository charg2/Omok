using GameServer.Models.User;
using GameServer.Repository;
using GameServer.Services.Interface;
using Shared;

namespace HiveServer.Services;


public class AuthService : IAuthService
{
    private readonly ILogger< AuthService > _logger;
    private readonly IMemoryDB _memoryDB;
    private readonly IGameDB _gameDB;
    private readonly HttpClient _httpClient;

    public AuthService( ILogger< AuthService > logger, IGameDB gameDB, IMemoryDB memoryDB, HttpClient httpClient )
    {
        _gameDB = gameDB;
        _memoryDB = memoryDB;
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri( "http://localhost:5071" );
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

}
