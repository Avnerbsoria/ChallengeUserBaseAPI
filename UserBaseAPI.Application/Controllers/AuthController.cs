using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;
using UserBaseAPI.Application.Core.Dtos;
using UserBaseAPI.Application.Core.Interfaces;
using UserBaseAPI.Application.Core.Services;

using UserBaseAPI.Domain.Extensions.ResultExtentions;

namespace UserBaseAPI.Application.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class AuthController : ControllerBase
{ 

    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;


    public AuthController(ILogger<AuthController> logger, IAuthService AuthService, IConfiguration configuration)
    {
        _logger = logger;
        _authService = AuthService;
        _configuration = configuration;
    }

    /// <summary>
    /// Log into the api
    /// </summary>
    /// <param name="login"></param>
    /// <returns>JWT Token</returns>
    [HttpPost("login")]
    public async Task<IActionResult> PostLogin(UserDto login)
    {
        var result = await _authService.Login(login, _configuration);

        Log.Information("User :"+login.Email+"loggeg at:"+ DateTime.Now);
        return this.FromResult(result);

    } 
}
