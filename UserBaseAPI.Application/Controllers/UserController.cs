using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using UserBaseAPI.Application.Core.Dtos;
using UserBaseAPI.Application.Core.Interfaces;
using UserBaseAPI.Application.Core.Services;

using UserBaseAPI.Domain.Extensions.ResultExtentions;

namespace UserBaseAPI.Application.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public string Role { get { return _httpContextAccessor?.HttpContext?.User.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role"); } }

    public UserController(ILogger<AuthController> logger, IUserService UserService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userService = UserService;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

    }
    /// <summary>
    /// Gets user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns>User</returns>
    [HttpGet("GetUserByEmail/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var result = await _userService.GetUserByEmail(email);
         
        return this.FromResult(result);
    }

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Confirmation</returns>
    //[Authorize(Policy = "anypolyceuwant")]
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto user)
    {
        try
        {
            var result = await _userService.CreateUser(user,this.Role);
            return this.FromResult(result);
        }
        catch (Exception e)
        {
            var ed = e;
            throw;
        }
    }

    /// <summary>
    /// Read all users from DB
    /// </summary>
    /// <returns></returns>
    [HttpGet("ReadUsers")]
    public async Task<IActionResult> ReadUsers()
    {
        try
        {
            var result = await _userService.ReadUsers();
            return this.FromResult(result);
        }
        catch (Exception e)
        {
            var ed = e;
            throw;
        }
    }

    /// <summary>
    /// Update a object of user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
    {
        try
        {
            var result = await _userService.UpdateUser(user, this.Role);
            return this.FromResult(result);
        }
        catch (Exception e)
        {
            var ed = e;
            throw;
        }
    }

    /// <summary>
    /// Mark a user as deleted but dont remove from DB
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] UserDto user)
    {
        try
        {
            var result = await _userService.DeleteUser(user.Id);
            return this.FromResult(result);
        }
        catch (Exception e)
        {
            var ed = e;
            throw;
        }
    }

}
