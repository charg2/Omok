using GameServer.DTO;
using GameServer.Repository;
using GameServer.Services.Interface;
using Microsoft.Extensions.Options;
using Shared;

namespace GameServer.Services;

public class MatchService : IMatchService
{
    private readonly ILogger< MatchService > _logger;
    private readonly IGameDB _gameDB;
    private readonly HttpClient _httpClient;
    private readonly IOptions< ServiceConfig > _serviceConfig;

    public MatchService( ILogger< MatchService > logger, IOptions< ServiceConfig > serviceConfig, IGameDB gameDB, HttpClient httpClient )
    {
        _logger = logger;
        _gameDB = gameDB;
        _serviceConfig = serviceConfig;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri( _serviceConfig.Value.MatchServer );
    }

    public async Task< ErrorCode > CancelMatch( string account, string token )
    {
        var response = await _httpClient.PostAsJsonAsync( "/api/CancelMatch", new CancelMatchReq()
        {
            Account = account,
            Token = token
        } );

        if ( !response.IsSuccessStatusCode )
        {
            _logger.LogWarning( $"Cancel match failed for user {account}: {response.StatusCode}" );
            return ErrorCode.None;
        }

        var cancelMatchRes = await response.Content.ReadFromJsonAsync< CancelMatchRes >();
        if ( cancelMatchRes is null )
        {
            _logger.LogWarning( $"Cancel match failed for user {account}" );
            return ErrorCode.UnknownError;
        }

        return ErrorCode.None;
    }

    public async Task< ErrorCode > StartMatch( string account, string token )
    {
        var response = await _httpClient.PostAsJsonAsync( "/api/StartMatch", new StartMatchReq()
        {
            Account = account,
            Token = token
        } );

        if ( !response.IsSuccessStatusCode )
        {
            _logger.LogWarning( $"Start match failed for user {account}: {response.StatusCode}" );
            return ErrorCode.UnknownError;
        }

        var startMatchRes = await response.Content.ReadFromJsonAsync< StartMatchRes >();
        if ( startMatchRes is null )
        {
            _logger.LogWarning( $"Start match failed for user {account}" );
            return ErrorCode.UnknownError;
        }

        return startMatchRes.Error;
    }

}
