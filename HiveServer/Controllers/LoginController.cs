using HiveServer.Models.User;
using HiveServer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HiveServer.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class LoginController : ControllerBase
{
    private readonly ILogger< RegisterController > _logger;
    private readonly IUserService _accountService;

    public LoginController( ILogger< RegisterController > logger, IUserService accountService )
    {
        _logger         = logger;
        _accountService = accountService;
    }


    [HttpPost]
    public async Task<HiveLoginRes> Post( [FromBody] HiveLoginReq request )
    {
        var (validateResult, token) = await _accountService.VaerifyUser( request.Account, request.Password );

        _logger.LogInformation( $"Login result: {validateResult}" );

        return new()
        {
            Account = request.Account,
            Password = request.Password,
            Token = token,
            Error = validateResult
        };
    }
}
